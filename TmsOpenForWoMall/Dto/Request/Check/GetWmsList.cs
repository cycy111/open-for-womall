using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zh.Common.ApiRequest;

namespace WmsReport.Dto.Request.Check
{
    /// <summary>
    /// 获取wms数据统计明细
    /// </summary>
    public class GetWmsList : PagingModel
    {
        /// <summary>
        /// 区域
        /// </summary>
        public string area { get; set; }

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
        /// 账号名称
        /// </summary>
        public string userName { get; set; }
    }
}
