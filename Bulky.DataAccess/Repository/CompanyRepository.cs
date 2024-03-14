using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private readonly ApplicationDbContext _db;
        public CompanyRepository(ApplicationDbContext Db) : base(Db)
        {
            _db=Db;
        }

        public void Update(Company obj)
        {
            var ObjFromdb = _db.Companies.FirstOrDefault(u => u.Id == obj.Id);


            if (obj != null)
            {
                ObjFromdb.StreetAddress= obj.StreetAddress;
                ObjFromdb.City= obj.City;
                ObjFromdb.PostalCode= obj.PostalCode;
                ObjFromdb.State= obj.State;
                ObjFromdb.Name= obj.Name;
                ObjFromdb.PhoneNumber= obj.PhoneNumber; 
            }

        }
    }
}
