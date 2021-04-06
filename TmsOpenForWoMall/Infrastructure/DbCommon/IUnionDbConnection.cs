using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TmsOpenForWoMall.Infrastructure.DbCommon
{
    /// <summary>
    /// 在DbConnection基础上再封装一层，访问联通数据库
    /// </summary>
    public interface IUnionDbConnection
    {
        /// <summary>
        /// 获取数据库连接字符串
        /// </summary>
        /// <returns></returns>
        string GetDbConnStr();
    }
}
