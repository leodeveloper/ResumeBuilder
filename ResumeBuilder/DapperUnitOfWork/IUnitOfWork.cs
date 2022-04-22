using System;
using System.Data;

namespace ResumeBuilder.DapperUnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IDbConnection Connection { get; }
      //  IDbTransaction Transaction { get; }
      //  void Commit();
       // void Dispose();
    }
}