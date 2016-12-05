using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GBSExtranet.Api.Models;
using Business;
using System.Configuration;
using System.Data.SqlClient;
namespace GBSExtranet.Api.ServiceLayer
{
    public class BaseService : IDisposable 
    {
        public Entities _db;
        public DataContext _context;
        public SqlConnection _sqlConnection;
        public BaseService()
        {
            _db = new Entities();
            _context = new DataContext(ConfigurationManager.ConnectionStrings["GBSConnection"].ConnectionString);
            _sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["GBSConnection"].ConnectionString);
        }
        public void SaveChanges()
        {
            _db.SaveChanges();
        }

        #region Disposal Methods

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}