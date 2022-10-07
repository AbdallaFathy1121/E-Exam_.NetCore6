using E_Exam.Core.Interfaces;
using E_Exam.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Exam.EF.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private ApplicationDbContext context;
        public BaseRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        // Method Get Data With All includes
        private IQueryable<T> GetDataWithIncludes(string[] includes)
        {
            IQueryable<T> query = context.Set<T>();

            if (includes != null)
                foreach (var item in includes)
                    query = query.Include(item);

            return query;
        }

        public async Task<IEnumerable<T>> GetAllAsync(string[]? includes = null)
        {
            IEnumerable<T> result;

            if (includes != null)
                result = await GetDataWithIncludes(includes).AsNoTracking().ToListAsync();
            else
                result = await context.Set<T>().AsNoTracking().ToListAsync();

            return result;
        }

        public async Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> criteria, string[]? includes = null)
        {
            if (includes != null)
                return await GetDataWithIncludes(includes).AsNoTracking().Where(criteria).ToListAsync();
            else
                return await context.Set<T>().AsNoTracking().Where(criteria).ToListAsync();
        }

        public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> criteria, string[]? includes = null)
        {
            if(includes != null)
                return await GetDataWithIncludes(includes).FirstOrDefaultAsync(criteria);
            else
                return await context.Set<T>().FirstOrDefaultAsync(criteria);
        }

        public async Task<bool> FindAsync(Expression<Func<T, bool>> criteria)
        {
            return await context.Set<T>().AsNoTracking().AnyAsync(criteria);
        }

        public async Task<bool> AddAsync(T entity)
        {
            try
            {
                await context.Set<T>().AddAsync(entity);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Update(T entity)
        {
            try
            {
                context.Update(entity);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(T entity)
        {
            try
            {
                context.Set<T>().Remove(entity);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public IQueryable<T> GetLeftJoinWithTbDepartmentLevel(int levelId)
        {
            var department = (from d in context.TbDepartments
                             join dl in context.TbDepartmentLevels
                             on d.Id equals dl.DepartmentId
                             into groub
                             from res in groub.DefaultIfEmpty()
                             where res.LevelId != levelId
                             select d).Distinct();

            IQueryable<T> t = (IQueryable<T>)department;

            return t;
        }
    }
}
