using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;

namespace PrinterManagerProject.EF
{
    public class BaseDALL<T> : IBaseDALL<T> where T : class
    {
        public PrintTagDbEntities DBContext = new PrintTagDbEntities();
        public void Add(T model)
        {
            DBContext.Set<T>().Add(model);
            DBContext.SaveChanges();
        }

        public void AddOrUpdate(T model)
        {

            DBContext.Set<T>().AddOrUpdate(model);
            DBContext.SaveChanges();
        }

        public T Find(int id)
        {
            return DBContext.Set<T>()
                .Find(id);
        }

        public T FirstOrDefault(Expression<Func<T, bool>> whereCondition)
        {
            return DBContext.Set<T>()
                .AsNoTracking()
                .Where(whereCondition)
                .FirstOrDefault();
        }

        public List<T> GetAll(Expression<Func<T, bool>> whereCondition)
        {
            return DBContext.Set<T>()
                .AsNoTracking()
                .Where(whereCondition)
                .ToList();
        }

        public List<T> GetAll()
        {
            return DBContext.Set<T>()
                .AsNoTracking()
                .ToList();
        }

        public void Update(T model)
        {
            DBContext.Set<T>().AddOrUpdate(model);
            DBContext.SaveChanges();
        }


        public bool Any(Expression<Func<T, bool>> whereCondition)
        {
            return DBContext.Set<T>()
                .AsNoTracking()
                .Any(whereCondition);
        }

        public void Delete(int id)
        {
            var model = DBContext.Set<T>().Find(id);
            if (model != null)
            {
                DBContext.Set<T>()
                    .Remove(model);
            }
        }
    }
}
