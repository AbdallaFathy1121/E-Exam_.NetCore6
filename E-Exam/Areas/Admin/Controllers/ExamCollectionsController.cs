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
            ViewBag.ExamId = examId;

            var result = await _unitOfWork.TbExamCollections.GetWhereAsync(a=>a.ExamId == examId && a.SubjectId == subjectId, new[] {"Exam", "ModelType", "Chapter"});
            
            // Using AutoMapper
            IEnumerable<ExamCollectionVM> model = _mapper.Map<IEnumerable<ExamCollectionVM>>(result);

            return View(model);
        }

        public async Task<IActionResult> Add(string id)
        {
            var split = id.Split("--");
            int examId = Convert.ToInt32(split[0]);
            int subjectId = Convert.ToInt32(split[1]);

            ViewBag.SubjectId = subjectId;
            ViewBag.ExamId = examId;
            ViewBag.Chapters = await _unitOfWork.TbChapters.GetAllAsync();
            ViewBag.ModelTypes = await _unitOfWork.TbModelTypes.GetAllAsync();

            return View("Edit");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var result = await _unitOfWork.TbExamCollections
                .GetFirstOrDefaultAsync(x => x.Id == id);

            ViewBag.SubjectId = result.SubjectId;
            ViewBag.ExamId = result.ExamId;
            ViewBag.Chapters = await _unitOfWork.TbChapters.GetAllAsync();
            ViewBag.ModelTypes = await _unitOfWork.TbModelTypes.GetAllAsync();

            if (result is not null)
            {
                ExamCollectionVM model = new()
                {
                    Id = result.Id,
                    NumberOfQuestions = result.NumberOfQuestions,
                    ChapterId = result.ChapterId,
                    ModelTypeId = result.ModelTypeId,
                };

                return View(model);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Save(ExamCollectionVM model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    TbExamCollection result = new()
                    {
                        SubjectId = model.SubjectId,
                        ExamId = model.ExamId,
                        NumberOfQuestions = model.NumberOfQuestions,
                        ModelTypeId = model.ModelTypeId,
                        ChapterId=model.ChapterId
                    };

                    await _unitOfWork.TbExamCollections.AddAsync(result);
                    _unitOfWork.Complete();

                    TempData["Success"] = "Add Exam Collection Successfully!";
                }
                else
                {
                    TbExamCollection result = new()
                    {
                        Id = model.Id,
                        SubjectId = model.SubjectId,
                        ExamId = model.ExamId,
                        NumberOfQuestions = model.NumberOfQuestions,
                        ModelTypeId = model.ModelTypeId,
                        ChapterId = model.ChapterId
                    };

                    _unitOfWork.TbExamCollections.Update(result);
                    _unitOfWork.Complete();

                    TempData["Success"] = "Update Exam Collection Successfully!";
                }

                return Redirect("/Admin/ExamCollections?id=" + model.ExamId+"--"+model.SubjectId);
            }

            ViewBag.SubjectId = model.SubjectId;
            ViewBag.ExamId = model.ExamId;
            ViewBag.Chapters = await _unitOfWork.TbChapters.GetAllAsync();
            ViewBag.ModelTypes = await _unitOfWork.TbModelTypes.GetAllAsync();

            return View("Edit", model);
        }






    }
}
