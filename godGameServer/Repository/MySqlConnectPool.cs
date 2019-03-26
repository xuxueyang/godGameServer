using MySql.Data.MySqlClient;
using System;

namespace godGameServer.Repository
{
    public class MySqlConnectPool
    {

        static string constr = "server=193.112.161.157;User Id=admin;password=admin;Database=godgame";
        private static MySqlConnection connection ;
        public static MySqlConnection Instance
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
        public static void main(string[] args)
        {
            MySqlCommand mycmd = new MySqlCommand("select * from game_acct;", MySqlConnectPool.Instance);
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
