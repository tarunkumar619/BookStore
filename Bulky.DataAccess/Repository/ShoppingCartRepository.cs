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
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {

      private readonly ApplicationDbContext _Db;
        public ShoppingCartRepository(ApplicationDbContext Db) : base(Db)
        {
            _Db = Db;
        }

        public void Update(ShoppingCart obj)
        {
            _Db.shoppingCarts.Update(obj);
        }
    }
}
