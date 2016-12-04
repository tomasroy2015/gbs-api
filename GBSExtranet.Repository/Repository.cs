using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GBSExtranet.Repository 
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> where);

        void Add(T entity);
        void Attach(T entity);
        void Delete(T entity);

        //long GetID(T entity);
    }
    public class Repository<T> : IRepository<T> where T : class
    {
        private DbSet<T> _entitySet;

        public Repository(DbContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            _entitySet = context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _entitySet;
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> where)
        {
            return _entitySet.Where(where);
        }

        public void Add(T entity)
        {
            _entitySet.Add(entity);
        }

        public void Attach(T entity)
        {
            _entitySet.Attach(entity);
        }

        public void Delete(T entity)
        {
            _entitySet.Remove(entity);
        }
    }
}
