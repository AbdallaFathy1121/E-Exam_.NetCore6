using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Exam.Core
{
    public interface IUnitOfWork : IDisposable
    {
        //IBaseRepository<Author> Authors { get; }
        //IBaseRepository<Book> Books { get; }

        int Complete();
    }
}
