using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WmsReport.Infrastructure.Config;

namespace TmsOpenForWoMall.Infrastructure.DbCommon
{
    /// <summary>
    /// 在DbConnection基础上再封装一层，访问联通数据库
    /// </summary>
    public class UnionDbConnection: IUnionDbConnection
    {
        //实现注入，基础数据库连接，配合Startup映射实现访问数据配置文件DbConnections
        private DbConnections _dbConnections;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbConnsAccessor"></param>
        public UnionDbConnection(IOptionsMonitor<DbConnections> dbConnsAccessor)
        {
            _dbConnections = dbConnsAccessor.CurrentValue;
        }

        /// <summary>
        /// 获取数据库连接字符串
        /// </summary>
        /// <returns></returns>
        public string GetDbConnStr()
        {
            string server = _dbConnections.UnionDb.server;
            string uid = _dbConnections.UnionDb.uid;
            string pwd = _dbConnections.UnionDb.pwd;
            string database = _dbConnections.UnionDb.database;
            return $"server={server};uid= {uid};pwd={pwd};database={database};";
        }
    }
}
