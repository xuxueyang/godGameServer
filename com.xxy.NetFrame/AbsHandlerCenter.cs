namespace com.xxy.NetFrame
{
    public abstract class AbsHandlerCenter
    {
        /// <summary>
        /// 客户端连接
        /// </summary>
        /// <param name="token"></param>
        public abstract void ClientConnect(UserToken token);
        public abstract void MessageReceive(UserToken token, object message);
        public abstract void ClientClose(UserToken token, string error);

    }
}
