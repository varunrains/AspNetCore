using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.IRepository;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BulkyBook.DataAccess.Repository
{
    public class CoverTypeRepository :  Repository<CoverType>, ICoverTypeRepository
    {

        private readonly ApplicationDbContext _db;

        public CoverTypeRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }

        public void Update(CoverType obj)
        {
            var dbObj = _db.CoverTypes.FirstOrDefault(ct => ct.Id == obj.Id);
            if (dbObj != null)
            {
                dbObj.Name = obj.Name;
                _db.SaveChanges();
            }
        }
    }
}
