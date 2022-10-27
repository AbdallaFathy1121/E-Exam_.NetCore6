using AutoMapper;
using E_Exam.Core;
using E_Exam.Core.ViewModels;
using E_Exam.Utility.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Exam.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Roles.Doctor)]
    public class ExamCollectionsController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ExamCollectionsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<IActionResult> Index(string id)
        {
            var split = id.Split("--");
            int examId = Convert.ToInt32(split[0]);
            int subjectId = Convert.ToInt32(split[1]);

            ViewBag.SubjectId = subjectId;

            var result = await _unitOfWork.TbExamCollections.GetWhereAsync(a=>a.ExamId == examId && a.SubjectId == subjectId, new[] {"Exam", "ModelType", "Chapter"});
            
            // Using AutoMapper
            IEnumerable<ExamCollectionVM> model = _mapper.Map<IEnumerable<ExamCollectionVM>>(result);

            return View(model);
        }



    }
}
