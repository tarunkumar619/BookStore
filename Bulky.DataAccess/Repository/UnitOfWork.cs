using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {

        public ApplicationDbContext db { get; set; } 
        public ICategoryRepository Category{ get; private set; }
        public IProductRepository Product { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
           this.db = db;
           Category= new CategoryRepository(db);
           Product = new ProductRepository(db);
        }
        public void save()
        {
            db.SaveChanges();
        }
    }
}
