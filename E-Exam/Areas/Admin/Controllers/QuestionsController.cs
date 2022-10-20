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
    public class QuestionsController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public QuestionsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<IActionResult> Index(int id)
        {
            var questions = await _unitOfWork.TbQuestions.GetWhereAsync(a => a.ModelId == id);
            ViewBag.ModelId = id;
            // Using AutoMapper
            IEnumerable<QuestionVM> model = _mapper.Map<IEnumerable<QuestionVM>>(questions);

            return View(model);
        }

        public IActionResult Add(int modelId)
        {
            ViewBag.ModelId = modelId;
            return View("Edit");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var result = await _unitOfWork.TbQuestions
                .GetFirstOrDefaultAsync(x => x.Id == id);

            ViewBag.ModelId = result.ModelId;

            if (result is not null)
            {
                // Using AutoMapper
                var model = _mapper.Map<QuestionVM>(result);

                return View(model);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Save(QuestionVM model)
        {
            if (ModelState.IsValid)
            {
                // Using AutoMapper
                var result = _mapper.Map<TbQuestion>(model);

                if (model.Id == 0)
                {
                    await _unitOfWork.TbQuestions.AddAsync(result);
                    TempData["Success"] = "Add New Question Successfully!";
                }
                else
                {
                    _unitOfWork.TbQuestions.Update(result);
                    TempData["Success"] = "Update Question Successfully!";
                }

                _unitOfWork.Complete();

                return Redirect("/Admin/Questions/Index/" + model.ModelId);
            }

            ViewBag.ModelId = model.ModelId;
            return View("Edit", model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var item = await _unitOfWork.TbQuestions.GetFirstOrDefaultAsync(a => a.Id == id);
            if (item is not null)
            {
                _unitOfWork.TbQuestions.Delete(item);
                _unitOfWork.Complete();
                TempData["Success"] = "Delete Question Successfully";

                return Json(new { success = true });
            }

            return Json(new { success = false, message = $"Not Found Question With ID: {id}" });
        }


    }
}
