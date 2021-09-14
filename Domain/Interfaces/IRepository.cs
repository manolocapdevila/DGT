using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> All();
        void Add(T TObject);
        void Delete(int id);
        void Update(T TObject);
        IEnumerable<T> Filter(Expression<Func<T, bool>> predicate);
        T GetById(int id);
        T Single(Expression<Func<T, bool>> expression);


    }
}
