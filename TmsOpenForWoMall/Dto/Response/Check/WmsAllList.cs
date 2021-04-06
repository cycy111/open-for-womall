using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WmsReport.Dto.Response.Check
{
    /// <summary>
    /// wms汇总数据
    /// </summary>
    public class WmsAllList
    {
        /// <summary>
        /// 部门
        /// </summary>
        public string deptCode { get; set; }

        /// <summary>
        /// 总储位数
        /// </summary>
        public string locCount { get; set; }

        /// <summary>
        /// 已用储位数
        /// </summary>
        public string resCount { get; set; }

        /// <summary>
        /// 储位使用率
        /// </summary>
        public string locRate { get; set; }

        /// <summary>
        /// 日盘点数
        /// </summary>
        public string dCheckCount { get; set; }

        /// <summary>
        /// 最近日盘点时间
        /// </summary>
        public string lastCheckDay { get; set; }

        /// <summary>
        /// 最近月盘点时间
        /// </summary>
        public string lastCheckMonth { get; set; }

        /// <summary>
        /// 最近抽盘时间
        /// </summary>
        public string lastCheckRandom { get; set; }

        /// <summary>
        /// 入库数量
        /// </summary>
        public string inQty { get; set; }

        /// <summary>
        /// 出库数量
        /// </summary>
        public string outQty { get; set; }

        /// <summary>
        /// 最近入库时间
        /// </summary>
        public string lastResTime { get; set; }

        /// <summary>
        /// 最近出库时间
        /// </summary>
        public string lastOutTime { get; set; }

        /// <summary>
        /// 最近对账时间
        /// </summary>
        public string lastAcountTime { get; set; }

        /// <summary>
        /// 对账数量
        /// </summary>
        public string acountCount { get; set; }

        

    }
}
