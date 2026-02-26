using Microsoft.EntityFrameworkCore;
using OfficeBooker.DataAccess.Data;
using OfficeBooker.DataAccess.Repository.IRepository;
using System.Linq.Expressions;


namespace OfficeBooker.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        public DbSet<T> dbSet { get; set; }

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            dbSet = _db.Set<T>();
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public async Task<T?> Get(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);
            return await query.FirstOrDefaultAsync(); 
        }

        public async  Task<IEnumerable<T>> GetAll()
        {
             return await dbSet.ToListAsync(); ;
        }

        public  void Remove(T entity)
        {
           _db.Remove(entity);
        }
    }
}
