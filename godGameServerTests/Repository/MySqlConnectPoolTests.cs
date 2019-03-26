using Microsoft.VisualStudio.TestTools.UnitTesting;
using godGameServer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace godGameServer.Repository.Tests
{
    [TestClass()]
    public class MySqlConnectPoolTests
    {
        [TestMethod()]
        public void mainTest()
        {
            MySqlCommand mycmd = new MySqlCommand("select * from game_acct;", MySqlConnectPool.Instance);
            if (mycmd.ExecuteNonQuery() > 0)
            {
                Console.WriteLine("数据插入成功！");
            }
            Console.ReadLine();
        }
    }
}