using E_Exam.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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




    }
}
