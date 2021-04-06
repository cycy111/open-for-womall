using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Zh.Common.DbHelper
{
    /// <summary>
    /// dapper帮助类
    /// </summary>
    public static class DapperHelper
    {
        /// <summary>
        /// 获取开启的数据库连接对象
        /// </summary>
        /// <param name="connStr">连接字符串</param>
        /// <param name="provider">数据库提供者的枚举</param>
        /// <returns></returns>
        public static IDbConnection GetOpenConnection(string connStr, DbProvider provider = DbProvider.SqlServer)
        {
            IDbConnection conn = null;
            switch (provider)
            {
                case DbProvider.SqlServer:
                    conn = new SqlConnection(connStr);
                    break;
                case DbProvider.Oracle:
                    conn = new OracleConnection(connStr);
                    break;
                case DbProvider.MySql:

                    break;
            }
            if (conn!=null)
            {
                conn.Open();
            }
            return conn;
        }
    }

    public enum DbProvider
    {
        SqlServer,
        Oracle,
        MySql
    }
}
