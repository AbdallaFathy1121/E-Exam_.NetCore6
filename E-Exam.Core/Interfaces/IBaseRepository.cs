using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Exam.Core.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(string[]? includes = null);
        Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> criteria, string[]? includes = null);
        Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> criteria, string[]? includes = null);
        IQueryable<T> GetLeftJoinWithTbDepartmentLevel(int levelId);
        Task<bool> FindAsync(Expression<Func<T, bool>> criteria);
        Task<bool> AddAsync(T entity);
        bool Update(T entity);
        bool Delete(T entity);
    }
}
