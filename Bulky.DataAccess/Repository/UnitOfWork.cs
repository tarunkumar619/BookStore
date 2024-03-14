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

        private ApplicationDbContext db { get; set; }
        public ICategoryRepository Category { get; private set; }
        public IProductRepository Product { get; private set; }

        public ICompanyRepository Company { get; private set; }

        public IShoppingCartRepository ShoppingCart { get; private set; }

        public IApplicationUserRepository ApplicationUser { get; private set; }

        public IOrderHeaderRepository OrderHeader { get; private set; }

        public IOrderDetailRepository OrderDetail { get; private set; }

        public UnitOfWork(ApplicationDbContext _db)
        {
            this.db = _db;
            Category = new CategoryRepository(this.db);
            Product = new ProductRepository(this.db);
            Company = new CompanyRepository(this.db);
            ShoppingCart = new ShoppingCartRepository(this.db);
            ApplicationUser = new ApplicationUserRepository(this.db);
            OrderHeader = new OrderHeaderRepository(this.db);
            OrderDetail = new OrderDetailRepository(this.db);

        }
        public void save()
        {
            db.SaveChanges();
        }
    }
}
