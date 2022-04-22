using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeBuilder.Dto
{
    public class IntegrationLogsDto
    {
        public Guid Rid { get; set; }
        public string IntegrationName { get; set; }
        public string Status { get; set; }
        public DateTime LastUpdateDateTime { get; set; }
        public string LastUpdateDateTimeFormatedString { get { return this.LastUpdateDateTime.ToString("yyyy-MM-dd HH:mm"); } }
        public string UserName { get; set; }
        public bool IsSuccessfull { get; set; }
        public long RowID { get; set; }
    }
}
