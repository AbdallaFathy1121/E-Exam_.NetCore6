using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Exam.Utility.EmailSender
{
    public interface IEmailSender
    {
        void SendEmail(string email, string subject, string htmlMessage);
    }
}
