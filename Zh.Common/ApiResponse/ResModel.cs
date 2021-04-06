using System;
using System.Collections.Generic;
using System.Text;

namespace Zh.Common.ApiRespose
{
    /// <summary>
    /// 接口调用时的响应模型。
    /// </summary>
    public class ResModel
    {
        /// <summary>
        /// api接口调用的返回码,返回值:true/false,默认为false
        /// </summary>
        public string success { get; set; } = "false";

        /// <summary>
        /// api接口调用的返回码。默认为1
        /// </summary>
        public string resultCode { get; set; } = "";

        /// api接口调用的返回信息,如果success=false时，将错误信息表述返回到该字段上
        /// </summary>
        public string resultMessage { get; set; } = "";

        public ResModel() { }

        public ResModel(string success, string message)
        {
            this.success = success;
            this.resultMessage = message;
        }

        /// <summary>
        /// 通过返回码和返回信息返回一个响应模型。
        /// </summary>
        /// <param name="resultCode"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public ResModel GetRes(string success, string message)
        {
            ResModel res = new ResModel();
            res.success = success;
            res.resultMessage = message;
            return res;
        }

    }
}
