using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WmsReport.Dto.Response.Check
{
    /// <summary>
    /// 抽盘盘点单
    /// </summary>
    public class ResCheckTask
    {
        /// <summary>
        /// 盘点单号
        /// </summary>
        public string checkNo { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public string deptCode { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public string deptName { get; set; }

        ///// <summary>
        ///// 盘点明细
        ///// </summary>
        //public List<ResCheckTaskDt> dt { get; set; }

        /// <summary>
        /// 物资编码
        /// </summary>
        public string ProductCode { get; set; }

        /// <summary>
        /// 物资名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string projectname { get; set; }
        

        /// <summary>
        /// 单位
        /// </summary>
        public string ProductUnit { get; set; }

        /// <summary>
        /// 储位
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// 账目库存
        /// </summary>
        public string KcQty { get; set; }

        /// <summary>
        /// 盘点数量
        /// </summary>
        public string checkQty { get; set; }

        /// <summary>
        /// 差异数量
        /// </summary>
        public string difQty { get; set; }

        /// <summary>
        /// 是否账实一致
        /// </summary>
        public string isSame { get; set; }

        /// <summary>
        /// 盘点状态
        /// </summary>
        public string checkStatus { get; set; }

        /// <summary>
        /// 盘点人
        /// </summary>
        public string checkUser { get; set; }

        /// <summary>
        /// 盘点时间
        /// </summary>
        public string checkDate { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string creator { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string createDate { get; set; }

        /// <summary>
        /// 创建者的姓名
        /// </summary>
        public string creatorName { get; set; }

        /// <summary>
        /// 推送人
        /// </summary>
        public string sender { get; set; }

        /// <summary>
        /// 推送时间
        /// </summary>
        public string sendDate { get; set; }

        public string senderName { get; set; }

        /// <summary>
        /// 超时时间(推送时间 + 1小时)
        /// </summary>
        public string overDate { get; set; }

        /// <summary>
        /// 主键Id
        /// </summary>
        public string cid { get; set; }

    }
}
