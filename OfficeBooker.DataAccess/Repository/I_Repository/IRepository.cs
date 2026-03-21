using System.Linq.Expressions;

namespace OfficeBooker.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? filter = null,string? includeProperties = null);
        Task<T?> Get(Expression<Func<T,bool>>filter);
        void Add(T entity);
        void  Remove(T entity);
    }
}