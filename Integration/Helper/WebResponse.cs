using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Integration.Helper
{
    public enum APIStatus
    {
        processing = 102,
        success = 200,
        cancelled = 203,
        aborted = 406,
        error = 500
    }

    public class WebResponse<T>: WebResponseNoData
    {
        public WebResponse()
        {
            this.data = default(T);
        }
        public T data { get; set; }
    }

    public class WebResponseNoData
    {
        public APIStatus status { get; set; }
        public string message { get; set; }       
    }

    public class WebResponsePaging<T> : WebResponseNoData
    {
        public long totalrecords { get; set; }
        public long totalpage
        {
            get
            {
                //if (pageSize < 1) return totalrecords / 10;

                if (pageSize == 0)
                    return 0;
                else
                {
                    if (totalrecords % pageSize == 0)
                        return totalrecords / pageSize;
                    else
                        return (totalrecords / pageSize) + 1;

                    //if (totalrecords % pageSize > 0)
                }
            }
        }
        public int currentpage { get { return this.page; } }
        public int nextpage
        {
            get
            {
                if (this.currentpage >= this.totalpage)
                    return 1;
                return this.currentpage + 1;
            }
        }
        public int previouspage
        {
            get
            {
                if (this.currentpage - 1 < 0)
                    return 0;
                return this.currentpage - 1;
            }
        }
        public int page { get; set; } = 1;
        public long pageSize { get; set; } = 1;
        public T data { get; set; }
    }
}
