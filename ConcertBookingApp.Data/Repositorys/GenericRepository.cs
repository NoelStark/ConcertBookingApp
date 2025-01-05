using ConcertBookingApp.Data.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ConcertBookingApp.Data.Repositorys
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly ApplicationDbContext _dbContext;
        public GenericRepository(ApplicationDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> expression)
        {
            return await _dbContext.Set<TEntity>().Where(expression).ToListAsync();
        }

        public async Task Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
            await Task.CompletedTask;
        }
        public async Task Delete(TEntity entity)
        {
            _dbContext.Remove(entity);
            await Task.CompletedTask;
        }
        public async Task Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            await Task.CompletedTask;
        }
        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }
    }
}
