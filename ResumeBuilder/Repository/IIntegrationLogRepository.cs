using ResumeBuilder.Dto;
using ResumeBuilder.Helper;
using ResumeBuilder.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using static ResumeBuilder.Helper.EnumHelper;

namespace ResumeBuilder.Repository
{
    public interface IIntegrationLogRepository
    {
        Task<Webresponse<IList<IntegrationLogsDto>>> GetintegrationLogsByIntegrationName(EnumHelper.IntegrationEnum initegrationName, long rowId);
        Task<bool> InsertintegrationLogs(IntegrationEnum integrationEnumName, bool IsSuccessfull, string status, long rowId);
    }
}