using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WmsReport.Infrastructure.Config
{
    /// <summary>
    /// 数据库连接配置映射类
    /// </summary>
    public class DbConnections
    {
        /// <summary>
        /// WMSReport数据库
        /// </summary>
        public string WMSReport { get; set; }

        /// <summary>
        /// tms数据库
        /// </summary>
        public string AccountCenter { get; set; }

        /// <summary>
        /// 数据库集合
        /// </summary>
        public WmsDb wmsDb { get; set; } = new WmsDb();

        /// <summary>
        /// 联通商场数据库
        /// </summary>
        public WmsDb UnionDb { get; set; } = new WmsDb();
    }

    /// <summary>
    /// wms数据库连接配置映射类
    /// </summary>
    public class WmsDb
    {
        public string server { get; set; }

        public string uid { get; set; }

        public string pwd { get; set; }

        public string database { get; set; }
    }
}
