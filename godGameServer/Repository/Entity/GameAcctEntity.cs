

using System;

namespace godGameServer.Repository.Entity
{
    public class GameAcctEntity
    {
        public string id;
        public string loginName;
        public string tel;
        public string email;
        public string password;
        public string loginIp;
        public string instanceCode;
        public string tenantCode;
        public DateTime createDate;
        public DateTime updateDate;
        public string isDeleted;
        public int version;
    }
}