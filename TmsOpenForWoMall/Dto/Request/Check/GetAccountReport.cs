using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zh.Common.ApiRequest;

namespace WmsReport.Dto.Request.Check
{
    /// <summary>
    /// 对账报表
    /// </summary>
    public class GetAccountReport : PagingModel
    {
        /// <summary>
        /// 盘点单号
        /// </summary>
        public string accountNo { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public string deptCode { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public string beginTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public string endTime { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string accountType { get; set; }

    }
}
