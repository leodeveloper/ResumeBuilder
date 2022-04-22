using System.Data;

namespace ResumeBuilder.DapperUnitOfWork
{
    public interface IUnitOfWorkHra
    {
        IDbConnection Connection { get; }
      
    }
}