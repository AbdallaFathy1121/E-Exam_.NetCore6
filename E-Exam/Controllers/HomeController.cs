using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace E_Exam.Controllers
{
    public class HomeController : Controller
    {
      

        public IActionResult Index()
        {
            return View();
        }

    
    }
}