using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WmsReport.Dto.Response.Check
{
    /// <summary>
    /// 未完成的盘点单
    /// </summary>
    public class UnOverCheckNo
    {
        /// <summary>
        /// 盘点单号
        /// </summary>
        public string checkNo { get; set; }

        /// <summary>
        /// 盘点状态
        /// </summary>
        public string checkstatus { get; set; }
        
    }
}
