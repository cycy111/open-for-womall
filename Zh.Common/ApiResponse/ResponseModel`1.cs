using System;
using System.Collections.Generic;
using System.Text;

namespace Zh.Common.ApiRespose
{
    /// <summary>
    /// api接口响应泛型模型。
    /// </summary>
    /// <typeparam name="TData">属性data的类型。</typeparam>
    public class ResponseModel<TData> :ResponseModel where TData:class
    {

        /// <summary>
        /// api接口返回的数据。
        /// </summary>
        public TData data {
            get; set; }

        public ResponseModel() { }

        public ResponseModel(int resultCode,string message,TData data):base(resultCode,message)
        {
            this.resultCode = resultCode;
            this.message = message;
            this.data = data;   
        }

        /// <summary>
        /// 根据参数，返回一个全新的Response对象。
        /// </summary>
        /// <param name="resultCode">返回码。</param>
        /// <param name="message">返回信息。</param>
        /// <param name="data">返回的数据。</param>
        /// <returns></returns>
        public virtual ResponseModel<TData> GetRes(int resultCode,string message,TData data = null)
        {
            ResponseModel<TData> res = new ResponseModel<TData>(resultCode,message,data);
            return res;
        }

        
    }
}
