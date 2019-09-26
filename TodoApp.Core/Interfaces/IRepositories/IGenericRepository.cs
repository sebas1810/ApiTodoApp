using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Core.Interfaces.IRepositories
{
    public interface IGenericRepository<T>
    {
        Task<T> GetAsync(int id);
        Task<T> GetAsync(Expression<Func<T, bool>> predicate);
        Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] navigationProperties);
        IQueryable<T> Query();
        Task<List<T>> Query(Expression<Func<T, bool>> predicate);
        Task<List<T>> Query(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] navigationProperties);
        Task InsertAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<bool> ExistAsync(int? input = 0);
    }
}
