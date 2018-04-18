using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace com.xxy.NetFrame
{
    /// <summary>
    /// 用户连接对象
    /// </summary>
    public class UserToken
    {
        /// <summary>
        /// 用户连接
        /// </summary>
        public Socket conn;
        /// <summary>
        /// 异步接受网络数据
        /// </summary>
        public SocketAsyncEventArgs receiverSAEA;
        /// <summary>
        /// 用户异步发送网络数据
        /// </summary>
        public SocketAsyncEventArgs sendSAEA;

        public LengthDecode LD;
        public LengthEncode LE;
        public encode encode;
        public decode decode;

        public delegate void SendProcess(SocketAsyncEventArgs e);
        public SendProcess sendProcess;
        public delegate void CloseProcess(UserToken token, string error);
        public CloseProcess closeProcess;

        public AbsHandlerCenter center;
        List<byte> cache = new List<byte>();

        private bool isReading = false;
        private bool isWriting = false;
        Queue<byte[]> writeQueue = new Queue<byte[]>();

        public UserToken()
        {
            receiverSAEA = new SocketAsyncEventArgs();
            sendSAEA = new SocketAsyncEventArgs();
            receiverSAEA.UserToken = this;//????所以，为什么不以接口方式实现呢？
            sendSAEA.UserToken = this;
            //设置接受对象的缓冲区大小
            receiverSAEA.SetBuffer(new byte[1024], 0, 1024);

        }
        /// <summary>
        /// 网络消息达到，异步
        /// </summary>
        /// <param name="buff"></param>
        public void receive(byte[] buff)
        {
            cache.AddRange(buff);
            if (!isReading)
            {
                isReading = true;
                onData();
            }
        }
        /// <summary>
        /// 处理数据
        /// </summary>
        private void onData()
        {
            //解码消息存储对象
            //流传输的是二进制，需要解码和编码
            byte[] buff = null;
            if (LD != null)
            {
                buff = LD(ref cache);
                //消息接受未接受全，退出，等待下次信息到达
                if (buff == null)
                {
                    isReading = false;
                    return;
                }
            }
            else
            {
                //这里相当于没解码直接取出。有点不好，万一什么什么呢？所以，这里我选择抛出错误。
                isReading = false;
                throw new Exception("length decode is null");
                /*
                if (cache.Count == 0)
                {
                    isReading = false;
                    return;
                }
                buff = cache.ToArray();
                cache.Clear();
                */
            }
            //操作数据
            if (decode == null)
            {
                throw new Exception("message decode is null");
            }
            //消息反序列化
            object message = decode(buff);
            //通知应用层有消息到达
            center.MessageReceive(this, message);
            //避免在处理中，又有消息到达
            onData();
        }

        public void write(byte[] value)
        {
            //发送消息完成
            if (conn == null)
            {
                closeProcess(this, "调用已经断开的连接");
            }
            writeQueue.Enqueue(value);
            if (!isWriting)
            {
                isWriting = true;
                onWrite();
            }
        }
        public void onWrite()
        {
            if (writeQueue.Count == 0)
            {
                isWriting = false;return;
            }
            byte[] buff = writeQueue.Dequeue();
            sendSAEA.SetBuffer(buff, 0, buff.Length);
            bool result = conn.SendAsync(sendSAEA);
            if (!result)
            {
                sendProcess(sendSAEA);
            }
        }
        public void writed()
        {
            onWrite();
        }
        public void Close()
        {
            try
            {
                writeQueue.Clear();
                cache.Clear();
                isWriting = false;
                isReading = false;
                conn.Shutdown(SocketShutdown.Both);
                conn.Close();
                conn = null;
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


    }
}
