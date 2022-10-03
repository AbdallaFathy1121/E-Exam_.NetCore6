using E_Exam.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Exam.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;
        //public IBaseRepository<Author> Authors { get; private set; }
        //public IBaseRepository<Book> Books { get; private set; }
        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
            //Authors = new BaseRepository<Author>(context);
            //Books = new BaseRepository<Book>(context);
        }

        public int Complete()
        {
            return context.SaveChanges();
        }
        public void Dispose()
        {
            context.Dispose();
        }
    }
}
