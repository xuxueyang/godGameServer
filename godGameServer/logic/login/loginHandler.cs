using com.xxy.NetFrame;
using com.xxy.NetFrame.auto;
using com.xxy.Protocol;
using com.xxy.Protocol.DTO;
using godGameServer.biz;
using godGameServer.tool;

namespace godGameServer.logic.login
{
    class loginHandler : AbsOneHandler, HandlerInterface
    {
        IAccountBiz accountBiz = BizFactory.accountBiz;
        public void ClientClose(UserToken token, string error)
        {

            ExecutorPool.Instance.execute(
            delegate ()
            {
                accountBiz.close(token);
            }
            );

        }

        public void ClientConnect(UserToken token)
        {

            //仅仅连接了套接字，未验证，不做处理

        }

        public void MessageReceive(UserToken token, SocketModel message)
        {

            switch (message.command)
            {
                case LoginProtocol.LOGIN_CREQ:
                    login(token, message.getMessage<AccountInfoDTO>());
                    break;
                case LoginProtocol.REG_CREQ:
                    reg(token, message.getMessage<AccountInfoDTO>());
                    break;
            }

        }
        //DTO传输模型
        public void login(UserToken token,AccountInfoDTO value)
        {

            ExecutorPool.Instance.execute(
            delegate ()
            {
                string result = accountBiz.login(token, value.accountName, value.password);
                write(token, LoginProtocol.LOGIN_SRES, result);
            }
            );

        }
        public void reg(UserToken token,AccountInfoDTO value)
        {

            ExecutorPool.Instance.execute(
            delegate ()
            {
                string result = accountBiz.create(token, value.accountName, value.password);
                write(token, LoginProtocol.REG_SRES, result);
            }
            );

        }
        public override byte GetGameType()
        {
            return Protocol.TYPE_LOGIN;
        }
    }
}
