using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace godGameServer.dao.model
{
    public class RoleModel
    {
        public int area;//所属大区编号
        public long id;
        public string name;
        public long accountId;//角色所属的账号ID
        public HashSet<string> friendSet;

        public RoleModel(string name,long id,long accountId)
        {
            this.name = name;this.id = id;this.accountId = accountId;
            friendSet = new HashSet<string>();
        }
    }
}
