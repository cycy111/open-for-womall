using System;
using System.Collections.Generic;
using System.Text;

namespace Zh.Common.ApiRequest
{
    public class PagingModel
    {
        public int pageIndex { get; set; } = 1;

        public int pageSize { get; set; } = 200;

        public int totalCount { get; set; } = 0;
    }
}
