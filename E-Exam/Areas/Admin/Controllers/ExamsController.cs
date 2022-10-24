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
            ViewBag.SubjectId = subjectId;
            // Using AutoMapper
            IEnumerable<ExamVM> model = _mapper.Map<IEnumerable<ExamVM>>(result);

            return View(model);
        }

        public async Task<IActionResult> Add(int id)
        {
            ViewBag.SubjectId = id;

            return View("Edit");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var result = await _unitOfWork.TbExams
                .GetFirstOrDefaultAsync(x => x.Id == id);

            ViewBag.SubjectId = result.SubjectId;

            if (result is not null)
            {
                ExamVM model = new()
                {
                    Id = result.Id,
                    SubjectId = result.SubjectId,
                    ExamName = result.ExamName,
                    StartDateTime = result.StartDateTime,
                    Duration = result.Duration
                };

                return View(model);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Save(ExamVM model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    TbExam result = new()
                    {
                        SubjectId = model.SubjectId,
                        ExamName = model.ExamName,
                        Duration = model.Duration,
                        StartDateTime = model.StartDateTime
                    };

                    await _unitOfWork.TbExams.AddAsync(result);
                    _unitOfWork.Complete();

                    TempData["Success"] = "Add Exam Successfully!";
                }
                else
                {
                    TbExam result = new()
                    {
                        Id = model.Id,
                        SubjectId = model.SubjectId,
                        ExamName = model.ExamName,
                        Duration = model.Duration,
                        StartDateTime = model.StartDateTime
                    };

                    _unitOfWork.TbExams.Update(result);
                    _unitOfWork.Complete();

                    TempData["Success"] = "Update Exam Successfully!";
                }

                return Redirect("/Admin/Exams?subjectId=" + model.SubjectId);
            }

            ViewBag.SubjectId = model.SubjectId;
            return View("Edit", model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var item = await _unitOfWork.TbExams.GetFirstOrDefaultAsync(x => x.Id == id);

            if (item != null)
            {
                _unitOfWork.TbExams.Delete(item);
                _unitOfWork.Complete();

                TempData["Success"] = "Delete Exam Successfully!";

                return Json(new { success = true });
            }

            return Json(new { success = false, message = $"Not Found Exam With ID: {id}" });
        }
    }
}
