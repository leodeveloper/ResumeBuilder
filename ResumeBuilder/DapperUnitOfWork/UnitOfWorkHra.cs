﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeBuilder.DapperUnitOfWork
{
    public class UnitOfWorkHra : IUnitOfWorkHra
    {
        private IDbConnection _connection;
        //private IDbTransaction _transaction;
        //private _repository;
        private bool _disposed;

        public UnitOfWorkHra(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
            // _connection.Open();
            //_transaction = _connection.BeginTransaction();
        }



        IDbConnection IUnitOfWorkHra.Connection
        {
            get { return _connection; }
        }
        //IDbTransaction IUnitOfWork.Transaction
        //{
        //    get { return _transaction; }
        //}

        public void Commit()
        {
            //try
            //{
            //    _transaction = _connection.BeginTransaction();
            //    _transaction.Commit();
            //}
            //catch
            //{
            //    _transaction.Rollback();
            //    throw;
            //}
            //finally
            //{
            //    _transaction.Dispose();
            //   // _transaction = _connection.BeginTransaction();

            //}
        }



        public void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
        }

        private void dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    //if (_transaction != null)
                    //{
                    //    _transaction.Dispose();
                    //    _transaction = null;
                    //}
                    //if (_connection != null)
                    //{
                    //    _connection.Dispose();
                    //    _connection = null;
                    //}
                }
                _disposed = true;
            }
        }

        //~UnitOfWork()
        //{
        //    dispose(false);
        //}
    }
}
