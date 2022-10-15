using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Exam.Core.ViewModels
{
    public class OperationResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public static OperationResult Succeeded(string msg)
        {
            return new OperationResult
            {
                Success = true,
                Message = msg
            };
        }

        public static OperationResult Error(string msg)
        {
            return new OperationResult
            {
                Success = false,
                Message = msg
            };
        }

    }
}
