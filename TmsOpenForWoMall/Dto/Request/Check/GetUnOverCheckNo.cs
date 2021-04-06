using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WmsReport.Dto.Request.Check
{
    /// <summary>
    /// 获取未完成的盘点单
    /// </summary>
    public class GetUnOverCheckNo
    {
        /// <summary>
        /// 部门
        /// </summary>
        public string deptCode { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public string userId { get; set; }
    }
}
