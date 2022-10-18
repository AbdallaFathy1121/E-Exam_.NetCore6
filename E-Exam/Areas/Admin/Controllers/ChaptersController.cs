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
    [Authorize(Roles = Roles.Admin)]
    public class ChaptersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ChaptersController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<IActionResult> Index()
        {
            var result = await _unitOfWork.TbChapters.GetAllAsync();
            // Using AutoMapper
            IEnumerable<ChapterVM> model = _mapper.Map<IEnumerable<ChapterVM>>(result);

            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var result = await _unitOfWork.TbChapters
                .GetFirstOrDefaultAsync(x => x.Id == id);

            if (result == null)
            {
                return View();
            }
            else
            {
                // Using AutoMapper
                var model = _mapper.Map<ChapterVM>(result);
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Save(ChapterVM model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    // Using AutoMapper
                    var result = _mapper.Map<TbChapter>(model);

                    await _unitOfWork.TbChapters.AddAsync(result);
                    TempData["Success"] = "Add New Chapter Successfully!";
                }
                else
                {
                    var item = await _unitOfWork.TbChapters.GetFirstOrDefaultAsync(x => x.Id == model.Id);
                    _unitOfWork.TbChapters.Update(item);
                    TempData["Success"] = "Update Chapter Successfully!";
                }

                _unitOfWork.Complete();

                return RedirectToAction("Index");
            }

            ViewBag.Levels = await _unitOfWork.TbLevels.GetAllAsync();
            return View("Edit", model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var item = await _unitOfWork.TbChapters.GetFirstOrDefaultAsync(x => x.Id == id);

            if (item != null)
            {
                _unitOfWork.TbChapters.Delete(item);
                _unitOfWork.Complete();

                TempData["Success"] = "Delete Chapter Successfully!";

                return Json(new { success = true });
            }

            return Json(new { success = false, message = $"Not Found Chapter With ID: {id}" });
        }

    }
}
