using TodoApp.Core.Interfaces.IRepositories;
using TodoApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Infrastructure.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T>
             where T : class, new()
    {
        protected TodoAppContext _dbContext { get; set; }

        public IQueryable<T> GetAll()
        {
            return _dbContext.Set<T>().AsQueryable();
        }
        public async Task<T> GetAsync(int id) => await _dbContext.FindAsync<T>(id);

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().Where(predicate).FirstOrDefaultAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] navigationProperties)
        {
            T item = null;
            IQueryable<T> dbQuery = _dbContext.Set<T>();

            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<T, object>(navigationProperty);

            item = await dbQuery
                .AsNoTracking() //Don't track any changes for the selected item
                .SingleOrDefaultAsync(predicate); //where clause
            return item;
        }
        public IQueryable<T> Query()
        {
            return _dbContext.Set<T>().AsQueryable();
        }
        public async Task<List<T>> Query(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = _dbContext.Set<T>().Where(predicate);

            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<T, object>(navigationProperty);

            var item = await dbQuery.AsNoTracking().ToListAsync(); //Don't track any changes for the selected item
            return item;
        }

        public async Task<List<T>> Query(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task InsertAsync(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Deleted;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> ExistAsync(int? input = 0)
        {
            return await _dbContext.TODO.AnyAsync(e => e.Id == input.Value);
        }
    }
}
