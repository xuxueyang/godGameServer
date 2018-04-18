

namespace com.xxy.Protocol
{
    public class MainRoomProtocol
    {
        //商店模块
        public const int SHOP_ENTER_CREQ = 11;
        public const int SHOP_ENTER_SRES = 12;
        public const int SHOP_BUY_CREQ = 13;
        public const int SHOP_BUY_SRES = 14;
        public const int SHOP_ADDMONEY_CREQ = 15;
        public const int SHOP_ADDMONEY_SRES = 16;

        //个人信息设置模块
        public const int PERSONAL_INFO_CHANGE_SIGN_CREQ = 21;
        public const int PERSONAL_INFO_CHANGE_SIGN_SRES = 22;


        //聊天模块
        public const int CHAT_BRO_CREQ = 41;//对所有在线的
        public const int CHAT_SOME_CREQ = 42;//对指定的
        public const int CHAT_SRES = 43;

        //好友模块
        public const int FRIEND_ADD_CREQ = 31;
        public const int FRIEND_ADD_SRES = 32;
        public const int FRIEND_DELETE_CREQ = 33;
        public const int FRIEND_DELETE_SRES = 34;
        //登陆、注册、获取、角色模块
        public const int GET_ROLE_INFO_CREQ = 51;
        public const int GET_ROLE_INFO_SRES = 52;
        public const int REG_ROLE_CREQ = 53;
        public const int REG_ROLE_SRES = 54;
        public const int LOGIN_ROLE_CREQ = 55;
        public const int LOGIN_ROLE_SRES = 55;


    }
}
