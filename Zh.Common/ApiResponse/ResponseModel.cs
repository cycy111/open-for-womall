using System;
using System.Collections.Generic;
using System.Text;

namespace Zh.Common.ApiRespose
{
    /// <summary>
    /// 接口调用时的响应模型。
    /// </summary>
    public class ResponseModel
    {
        /// <summary>
        /// api接口调用的返回码。默认为1
        /// </summary>
        public int resultCode { get; set; } = 1;

        /// <summary>
        /// api接口调用的返回码。默认为''
        /// </summary>
        public string resultCodes { get; set; } = "";
        /// <summary>
        /// api接口调用的返回信息。
        /// </summary>
        public string message { get; set; } = "success";

        public ResponseModel() { }

        public ResponseModel(int resultCode,string message)
        {
            this.resultCode = resultCode;
            this.message = message;
        }

        /// <summary>
        /// 通过返回码和返回信息返回一个响应模型。
        /// </summary>
        /// <param name="resultCode"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public ResponseModel GetRes(int resultCode,string message)
        {
            ResponseModel res = new ResponseModel();
            res.resultCode = resultCode;
            res.message = message;
            return res;
        }

    }
}
