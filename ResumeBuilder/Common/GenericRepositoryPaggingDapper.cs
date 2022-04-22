using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using ResumeBuilder.Models;
using ResumeBuilder.DapperUnitOfWork;

namespace ResumeBuilder
{
    public class GenericRepositoryPaggingDapper<T> : IGenericRepositoryPaggingDapper<T> where T : class
    {
        readonly IUnitOfWork _iUnitOfWork;
        public GenericRepositoryPaggingDapper(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        public async Task<IEnumerable<T>> GetAllPagedAsync(string ordrbyCloumnsName, IDbConnection _dbconnection, int pageNumber = 1, int rowCount = 10)
        {
            try
            {
                var tableName = typeof(T).Name;
                // assuming here you want the newest rows first, and column name is "created_date"
                // may also wish to specify the exact columns needed, rather than *
                var query = ($" select * from {tableName} order by {ordrbyCloumnsName} offset ({pageNumber} - 1) * {rowCount} rows fetch next {rowCount} rows only");
                var results = await _iUnitOfWork.Connection.QueryAsync<T>(query, null);
                return results;
            }
            catch(Exception ex)
            {
                throw ex;
            }
           
        }

        public async Task<WebresponsePaging<IList<T>>> GetAllPagedTotalCountAsync(string ordrbyCloumnsName, int pageNumber = 1, int rowCount = 10)
        {
            WebresponsePaging<IList<T>> response = new WebresponsePaging<IList<T>>();
            try
            {
                if (pageNumber <= 0)
                    pageNumber = 1;

                var tableName = typeof(T).Name;
                // assuming here you want the newest rows first, and column name is "created_date"
                // may also wish to specify the exact columns needed, rather than *
                //https://andypalmer.dev/posts/pagination-with-dapper/
                var queryTotalRecord = ($" select Count(*) from {tableName}");
                var query = ($" select * from {tableName} order by {ordrbyCloumnsName} desc offset ({pageNumber} - 1) * {rowCount} rows fetch next {rowCount} rows only; " + queryTotalRecord);
                var results = await _iUnitOfWork.Connection.QueryMultipleAsync(query, null);

                var rows = results.Read<T>().ToList();
                var totalRowCount = results.Read<long>().Single();

                response.status = APIStatus.success;
                response.data = rows;
                response.pageSize = rowCount;
                response.page = pageNumber;
                response.totalrecords = totalRowCount;
            }
            catch(Exception ex)
            {
                response.status = APIStatus.error;
                response.message = ex.Message;
            }
            return response;
        }

       
    }

    static class PagingUtils
    {
        public static IEnumerable<T> Page<T>(this IEnumerable<T> en, int pageSize, int page)
        {
            return en.Skip(page * pageSize).Take(pageSize);
        }
        public static IQueryable<T> Page<T>(this IQueryable<T> en, int pageSize, int page)
        {
            return en.Skip(page * pageSize).Take(pageSize);
        }
    }


}
