using System;
using System.Collections.Generic;
using System.Text;

namespace OfficeBooker.DataAccess.Repository.I_Repository
{
    public interface IUnitOfWork 
    {
        void Save();
    }
}
