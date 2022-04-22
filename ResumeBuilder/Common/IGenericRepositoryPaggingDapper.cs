using ResumeBuilder.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ResumeBuilder
{
    public interface IGenericRepositoryPaggingDapper<T> where T : class
    {
        Task<IEnumerable<T>> GetAllPagedAsync(string ordrbyCloumnsName, IDbConnection _dbconnection, int pageNumber = 1, int rowCount = 10);
        Task<WebresponsePaging<IList<T>>> GetAllPagedTotalCountAsync( string ordrbyCloumnsName, int pageNumber = 1, int rowCount = 10);
    }
}