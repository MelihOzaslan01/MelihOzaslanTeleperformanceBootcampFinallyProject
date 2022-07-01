using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Shopping.Application.Common.Interfaces;
using Shopping.Domain.Entities;

namespace Shopping.Infrastructure.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T>
        where T : class,IBaseEntity,new()
    {

        protected readonly ApplicationDbContext _applicationDbContext;
        protected readonly DbSet<T> _dbSet;
        public GenericRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _dbSet = _applicationDbContext.Set<T>();
        }
        

        public async Task<bool> Add(T entity)
        {
            await _dbSet.AddAsync(entity);
            var result = await _applicationDbContext.SaveChangesAsync();
            return result>0;
        }

        public async Task<bool> Delete(T entity)
        {
            var current = await _dbSet.FirstOrDefaultAsync(x => x.Id == entity.Id);
            if (current == null) throw new Exception("Entity not found!");

            _dbSet.Remove(current);
            var result = await _applicationDbContext.SaveChangesAsync();
            return result>0;
        }

        public async Task<bool> Update(T entity)
        {
            _dbSet.Update(entity);
            var result = await _applicationDbContext.SaveChangesAsync();
            return result>0;
        }
        
        public async Task<IEnumerable<T>> Get(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.Where(expression).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _dbSet.SingleOrDefaultAsync(x => x.Id == id);
        }

   
    }
}
