using OfficeBooker.DataAccess.Data;
using OfficeBooker.DataAccess.Repository.I_Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace OfficeBooker.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            
        }
        public void Save()
        {
            _db.SaveChanges();

        }
    }
}
