using System.Linq.Expressions;
using Shopping.Domain.Entities;

namespace Shopping.Application.Common.Interfaces
{

    public interface IGenericRepository<T>: IGenericCommandRepository<T>, 
        IGenericQueryRepository<T> where T : class,IBaseEntity,new()
    {

    }
    public interface IGenericCommandRepository<T> where T : class
    {
        Task<bool> Add(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(T entity);
    }

    public interface IGenericQueryRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task<IEnumerable<T>> Get(Expression<Func<T,bool>> expression);

    }
}
