using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace godGameServer.Repository
{
    public class MySqlConnectPool
    {

        static string constr = "server=193.112.161.157;User Id=root;password=XXY@tx.2018!;Database=GodGame";
        private static MySqlConnection connection ;
        private static MySqlConnection Instance
        {
            get
            {
                if (connection == null)
                {
                    connection = new MySqlConnection(constr);
                    connection.Open();
                }                  
                return connection;
            }
        }
        private MySqlConnectPool()
        {

        }
        static void main(string[] args)
        {
            MySqlCommand mycmd = new MySqlCommand("insert into test(id,name) values('90','abc')", MySqlConnectPool.Instance);
            if (mycmd.ExecuteNonQuery() > 0)
            {
                Console.WriteLine("数据插入成功！");
            }
            Console.ReadLine();
            
        }

        ~MySqlConnectPool()
        {
            MySqlConnectPool.Instance.Close();
        }
    }
}
