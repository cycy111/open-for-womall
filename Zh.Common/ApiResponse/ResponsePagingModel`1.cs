using System;
using System.Collections.Generic;
using System.Text;
using Zh.Common.ApiRespose;

namespace Zh.Common.ApiResponse
{
    public class ResponsePagingModel<TData>:ResponseModel<TData> where TData : class
    {
        public int pageIndex { get; set; } = 1;

        public int pageSize { get; set; } = 10;

        public int counts { get; set; } = 0;

        public ResponsePagingModel<TData> GetRes(int resultCode, string message, TData data = null,int totalCount = 0,int pageIndex = 0,int pageSize = 0)
        {
            ResponsePagingModel<TData> res = new ResponsePagingModel<TData>();
            res.resultCode = resultCode;
            res.message = message;
            res.data = data;
            res.counts = counts;
            res.pageIndex = pageIndex;
            res.pageSize = pageSize;
            return res;
        }
       
    }
}
