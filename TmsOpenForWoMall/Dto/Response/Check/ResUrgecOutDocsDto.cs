using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WmsReport.Dto.Response.Check
{
    public class ResUrgecOutDocsDto
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string rid { get; set; }

        /// <summary>
        /// 系统单号
        /// </summary>
        public string outCode { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        public string checkName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string createDate { get; set; }

        /// <summary>
        /// 申请人
        /// </summary>
        public string applyName { get; set; }

        /// <summary>
        /// 出库状态
        /// </summary>
        public string outStatus { get; set; }

        /// <summary>
        /// 单据状态
        /// </summary>
        public string ledCheckStatus { get; set; }

        /// <summary>
        /// 出库类型
        /// </summary>
        public string outType { get; set; }

        /// <summary>
        /// 物资类别
        /// </summary>
        public string productType { get; set; }

        /// <summary>
        /// 项目编号
        /// </summary>
        public string projectCode { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string projectName { get; set; }

        /// <summary>
        /// 需求部门
        /// </summary>
        public string dptName { get; set; }
    }
}
