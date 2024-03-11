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
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext Db) : base(Db)
        {
            _db = Db;
        }

        public void Update(Product obj)
        {
          var ObjFromdb = _db.Products.FirstOrDefault(u=>u.Id == obj.Id); 

            if(obj != null) { 
                    ObjFromdb.Title=obj.Title;  
                    ObjFromdb.Description=obj.Description;  
                    ObjFromdb.ISBN  = obj.ISBN; 
                    ObjFromdb.Price =  obj.Price;    
                    ObjFromdb.ListPrice=obj.ListPrice;  
                    ObjFromdb.Price100=obj.Price100;    
                    ObjFromdb.CategoryId=obj.CategoryId;
                    ObjFromdb.Author = obj.Author;
                if (obj.ImageUrl != null)
                {
                    ObjFromdb.ImageUrl=obj.ImageUrl;    
                }

            }
        }
    }
}
