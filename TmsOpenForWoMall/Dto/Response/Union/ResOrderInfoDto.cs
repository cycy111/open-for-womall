using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TmsOpenForWoMall.Dto.Response.Union
{
    /// <summary>
    /// 物流查询接口请求返回类
    /// </summary>
    public class ResOrderInfoDto
    {
        /// <summary>
        /// 物流单号
        /// </summary>
        public string logisticsNo { get; set; }

        /// <summary>
        /// 是否妥投0否1是
        /// </summary>
        public string isDelivered { get; set; }

        /// <summary>
        /// 物流配送信息
        /// </summary>
        public List<orderTrack> orderTracks { get; set; }

    }

    /// <summary>
    /// 物流配送实体类
    /// </summary>
    public class orderTrack
    {
       
        private DateTime _msgtime;

        /// <summary>
        /// 操作时间yyyy-MM-dd HH:mm:ss
        /// </summary>		
        public DateTime msgTime
        {
            get { return _msgtime; }
            set { _msgtime = value; }
        }
        	
        private string _content;

        /// <summary>
        /// 配送内容
        /// </summary>	
        public string content
        {
            get { return _content; }
            set { _content = value; }
        }

    }

}
