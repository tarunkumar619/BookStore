    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using Bulky.DataAccess.Data;
    using Bulky.DataAccess.Repository.IRepository;
    using Microsoft.EntityFrameworkCore;
    using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

    namespace Bulky.DataAccess.Repository
    {
        public class Repository<T> : IRepository<T> where T : class
        {
            private readonly ApplicationDbContext _Db;
            internal DbSet<T> DbSet;  


            public Repository(ApplicationDbContext Db)
            {
             _Db = Db;  
              this.DbSet=_Db.Set<T>();         
        }

            public void Add(T entity)
            {
              DbSet.Add(entity);
            }

            public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null)
            {
                IQueryable<T> query = DbSet;
                query=query.Where(filter);
                if (!string.IsNullOrEmpty(includeProperties))
                {
                    foreach (var includeProp in includeProperties
                        .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(includeProp);
                    }
                }
                return query.FirstOrDefault();


            }

            public IEnumerable<T> GetAll(string? includeProperties = null)
            {
                IQueryable<T> query = DbSet;
                if (!string.IsNullOrEmpty(includeProperties))
                {
            foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(includeProp);
                    }
                }
                
            return query.ToList(); 
            }

            public void Remove(T entity)
            {
                DbSet.Remove(entity);
            }

            public void RemoveRange(IEnumerable<T> entity)
            {
                DbSet.RemoveRange();    
            }
        }
    }
