using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace com.xxy.NetFrame
{
    public class ServerStart
    {
        public LengthDecode LD;
        public LengthEncode LE;
        public encode encode;
        public decode decode;
        public AbsHandlerCenter center;
        UserTokenPool pool;
        Socket server;
        int maxClient;//玩家最大连接数
        Semaphore acceptClients;//添加信号量，以避免冲突


        public ServerStart(int max)
        {
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.maxClient = max;

        }
        public void start(int port)
        {
            pool = new UserTokenPool(maxClient);
            //连接信号量
            acceptClients = new Semaphore(maxClient, maxClient);
            for(int i = 0; i < maxClient; i++)
            {
                UserToken token = new UserToken();
                token.receiverSAEA.Completed += new EventHandler<SocketAsyncEventArgs>(IO_Completed);
                token.sendSAEA.Completed += new EventHandler<SocketAsyncEventArgs>(IO_Completed);
                token.LD = this.LD;
                token.LE = this.LE;
                token.encode = this.encode;
                token.decode = this.decode;
                token.sendProcess = ProcessSend;
                token.closeProcess = ClientClose;
                token.center = center;
                pool.push(token);
            }
            try
            {
                server.Bind(new IPEndPoint(IPAddress.Any, port));
                server.Listen(10);
                StartAccept(null);
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void StartAccept(SocketAsyncEventArgs e)
        {
            if (e == null)
            {
                e = new SocketAsyncEventArgs();
                e.Completed += new EventHandler<SocketAsyncEventArgs>(Accept_Completed);
            }
            else
            {
                e.AcceptSocket = null;
            }
            //信号量
            acceptClients.WaitOne();
            bool result = server.AcceptAsync(e);
            //判断异步事件是否休战，没挂机说明立刻执行完成。直接处理事件，否则完成后触发
            if (!result)
            {
                ProcessAccept(e);
            }
        }
        public void Accept_Completed(object sender, SocketAsyncEventArgs e)
        {
            ProcessAccept(e);
        }
        public void ProcessAccept(SocketAsyncEventArgs e)
        {
            //处理连接事件
            //分配连接对象，供用户使用
            UserToken token = pool.pop();
            center.ClientConnect(token);
            token.conn = e.AcceptSocket;
            StartReceive(token);
            StartAccept(e);
        }
        public void StartReceive(UserToken token)
        {
            try
            {
                bool result = token.conn.ReceiveAsync(token.receiverSAEA);
                if (!result)
                {
                    ProcessReceive(token.receiverSAEA);
                }
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void  IO_Completed(object sender,SocketAsyncEventArgs e)
        {
            if (e.LastOperation == SocketAsyncOperation.Receive)
            {
                ProcessReceive(e);
            }
            else
            {
                ProcessSend(e);
            }
        }


        public void ProcessReceive(SocketAsyncEventArgs e)
        {
            UserToken token = e.UserToken as UserToken;
            if (token.receiverSAEA.BytesTransferred > 0 && token.receiverSAEA.SocketError == SocketError.Success)
            {
                byte[] message = new byte[token.receiverSAEA.BytesTransferred];
                Buffer.BlockCopy(token.receiverSAEA.Buffer, 0, message,0, token.receiverSAEA.BytesTransferred);
                try
                {
                    //TODO 这里可能出现解析错误！
                    token.receive(message);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    ClientClose(token, "客户端数据发送格式出错！");
                }
                StartReceive(token);
            }
            else
            {
                if (token.receiverSAEA.SocketError != SocketError.Success)
                {
                    ClientClose(token, token.receiverSAEA.SocketError.ToString());
                }
                else
                {
                    //此时客户端异常断开
                    ClientClose(token, "客户端主动断开连接");
                }
            }
        }
        public void ProcessSend(SocketAsyncEventArgs e)
        {
            UserToken token = e.UserToken as UserToken;
            if (e.SocketError != SocketError.Success)
            {
                ClientClose(token, e.SocketError.ToString());
            }
            {
                //消息发送成功，回调
                token.writed();
            }
        }
        public void ClientClose(UserToken token,string error)
        {
            if (token.conn != null)
            {
                lock (token)
                {
                    center.ClientClose(token, error);
                    token.Close();
                    pool.push(token);
                    acceptClients.Release();
                }
            }
        }

    }
}
