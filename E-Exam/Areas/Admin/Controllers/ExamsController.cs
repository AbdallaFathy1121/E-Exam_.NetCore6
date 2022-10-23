using AutoMapper;
using E_Exam.Core;
using E_Exam.Core.Models;
using E_Exam.Core.ViewModels;
using E_Exam.Utility.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Exam.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Roles.Doctor)]
    public class ExamsController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ExamsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<IActionResult> Index(int subjectId)
        {
            var result = await _unitOfWork.TbExams.GetWhereAsync(a => a.SubjectId == subjectId);
            // Using AutoMapper
            IEnumerable<ExamVM> model = _mapper.Map<IEnumerable<ExamVM>>(result);

            return View(model);
        }







    }
}
