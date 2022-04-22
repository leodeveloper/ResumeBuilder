using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeBuilder.Models
{
    public enum APIStatus
    {
        processing = 102,
        success = 200,
        cancelled = 203,
        aborted = 406,
        error = 500,
        NotFound = 404
    }

    public class Webresponse<T> : WebresponseNoData
    {
        public T data { get; set; }
    }

    public class WebresponseNoData
    {
        public APIStatus status { get; set; }
        public string message { get; set; }

    }

    public class WebresponsePaging<T> : WebresponseNoData
    {
        public long totalrecords { get; set; }
        public long totalpage
        {
            get
            {
                if (this.pageSize < 1)
                    return this.totalrecords / 10;
                return this.totalrecords / this.pageSize;
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
