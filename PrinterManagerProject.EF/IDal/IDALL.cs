using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PrinterManagerProject.EF
{
    public interface IBaseDALL<T> where T : class
    {
        T Find(int id);
        T FirstOrDefault(Expression<Func<T, bool>> whereCondition);
        List<T> GetAll(Expression<Func<T, bool>> whereCondition);
        List<T> GetAll();
        void Add(T model);
        void Update(T model);
        void AddOrUpdate(T model);
        bool Any(Expression<Func<T, bool>> whereCondition);
        void Delete(int id);
        IQueryable<T> GetQueryable();
    }
}
