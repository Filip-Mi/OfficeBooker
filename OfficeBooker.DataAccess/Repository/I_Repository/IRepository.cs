using System.Linq.Expressions;

namespace OfficeBooker.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        Task<T?> Get(Expression<Func<T,bool>>filter);
        void Add(T entity);
        void  Remove(T entity);
    }
}
