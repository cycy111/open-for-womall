using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zh.Common.ApiRequest;

namespace WmsReport.Dto.Request.Check
{
    /// <summary>
    /// 获取盘点报表明细 
    /// </summary>
    public class GetCheckReport : PagingModel
    {
        /// <summary>
        /// 盘点单号
        /// </summary>
        public string checkNo { get; set; }

        /// <summary>
        /// 盘点类型
        /// </summary>
        public string checkType { get; set; }

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


    }
}
