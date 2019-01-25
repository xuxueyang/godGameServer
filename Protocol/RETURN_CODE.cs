

namespace com.xxy.Protocol
{
    public class RETURN_CODE
    {
        public static readonly string SUCCESS = "0—成功";
        //通用的以1开头
        public static readonly string INPUT_IS_ILLEGAL = "1001—输入非法";

        //login模块以4开头
        public static readonly string ACCOUNT_IS_NOT_EXIST = "4001—账户不存在";
        public static readonly string ACCOUNT_NAME_HAS_EXIST = "4004—账户名字已存在";
        public static readonly string ACCOUNT_HAS_ONLINE = "4005—账户已经在线";
        public static readonly string ACCOUNT_IS_NOT_MATCH = "4006—账户和密码不匹配";
        //mainRoom模块
        public static readonly string MAIN_ROOM_FRIEND_ADD_ROLE_NOT_EXIST = "3001—添加的好友不存在";
        public static readonly string ACCOUNT_HAS_ROLE = "3002—账户已经拥有角色";
        public static readonly string ROLE_NAME_HAS_EXIST = "3003—角色名字已存在";
        public static readonly string MAIN_ROOM_DRIEND_DELETE_ROLE_NOT_EXIST = "3004—删除的好友不存在";
        // 战斗房间的
        public static readonly string BAALE_ROOM_CAEATE_ERROR = "5001—有人终止了匹配，请重试";

    }
}
