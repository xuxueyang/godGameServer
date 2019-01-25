using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace godGameServer.dao.model
{
    public class AccountModel
    {
        //用户信息
        public int area;//进入的大区号
        private long id;
        public string name;
        private string password;
        public AccountModel(string name,string password,long id)
        {
            this.name = name;this.password = password;
            this.id = id;
        }
        public void setArea(int area)
        {
            this.area = area;
        }
        public long getId() { return id; }
        public string getPassword() { return this.password; }
    }
}
