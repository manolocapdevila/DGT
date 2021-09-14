using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Domain.Core
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private DataContext _db;

        public Repository(DataContext context) 
        {
            _db = context;
        }

        public void Add(T TObject)
        {
            var newEntry = _db.Set<T>().Add(TObject);
            newEntry.State = EntityState.Added;
        }

        public IQueryable<T> All()
        {
            return _db.Set<T>().AsNoTracking().AsQueryable<T>();
        }

        public void Delete(int id)
        {
            var TObject = GetById(id);
            if (TObject == null)
                return;
            _db.Set<T>().Remove(TObject);
        }

        public IEnumerable<T> Filter(Expression<Func<T, bool>> predicate)
        {
            return _db.Set<T>().Where<T>(predicate).AsNoTracking().AsQueryable<T>();
        }

        public T GetById(int id)
        {
            return _db.Set<T>().Find(id);
        }

        public void Update(T TObject)
        {

            var entry = _db.Entry(TObject);
            _db.Set<T>().Attach(TObject);
            entry.State = EntityState.Modified;
        }

        public T Single(Expression<Func<T, bool>> expression)
        {
            return All().AsNoTracking().FirstOrDefault(expression);
        }
    }
}
