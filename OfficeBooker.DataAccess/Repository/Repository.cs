using Microsoft.EntityFrameworkCore;
using OfficeBooker.DataAccess.Data;
using OfficeBooker.DataAccess.Repository.IRepository;


namespace OfficeBooker.DataAccess.Repository
{
    internal class Repository<T> : IRepository<T> where T : class
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

        public T Get(T entity)
        {
            IQueryable<T> query = dbSet;
            return query.FirstOrDefault(u=>u.Equals(entity));
        }

        public IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }

        public void Remove(T entity)
        {
            _db.Remove(entity);
        }
    }
}
