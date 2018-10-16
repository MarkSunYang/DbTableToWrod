using MySql.Data.MySqlClient;
using System;
using System.Data.Common;
using System.Data.SqlClient;

namespace DbFieldToWorld.ConectionFactory
{
    /// <summary>
    /// 
    /// </summary>
    public class ConnectionFactory
    {
        private static MySqlConnection mysqlcon = new MySqlConnection("数据库连接字符串");
        private static SqlConnection sqlservercon = new SqlConnection("数据库连接字符串");

        /// <summary>
        /// 获取mysql连接字符串
        /// </summary>
        public DbConnection MySqlDbConnection() 
        {
            return mysqlcon;
        }

        /// <summary>
        /// 获取sqlserver 连接
        /// </summary>
        public DbConnection SqlServerDbConnection()
        {
            return sqlservercon;
        }

    }

}
