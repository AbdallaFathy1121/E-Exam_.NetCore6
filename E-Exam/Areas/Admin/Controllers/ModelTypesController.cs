using AutoMapper;
using E_Exam.Core.Models;
using E_Exam.Core.ViewModels;
using E_Exam.Core;
using E_Exam.Utility.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Exam.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Roles.Admin)]
    public class ModelTypesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ModelTypesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<IActionResult> Index()
        {
            var result = await _unitOfWork.TbModelTypes.GetAllAsync();
            // Using AutoMapper
            IEnumerable<ModelTypeVM> model = _mapper.Map<IEnumerable<ModelTypeVM>>(result);

            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var result = await _unitOfWork.TbModelTypes
                .GetFirstOrDefaultAsync(x => x.Id == id);

            if (result == null)
            {
                return View();
            }
            else
            {
                // Using AutoMapper
                var model = _mapper.Map<ModelTypeVM>(result);
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Save(ModelTypeVM model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    // Using AutoMapper
                    var result = _mapper.Map<TbModelType>(model);

                    await _unitOfWork.TbModelTypes.AddAsync(result);
                    TempData["Success"] = "Add New ModelType Successfully!";
                }
                else
                {
                    var item = await _unitOfWork.TbModelTypes.GetFirstOrDefaultAsync(x => x.Id == model.Id);
                    item.Type = model.Type;
                    _unitOfWork.TbModelTypes.Update(item);
                    TempData["Success"] = "Update ModelType Successfully!";
                }

                _unitOfWork.Complete();

                return RedirectToAction("Index");
            }

            ViewBag.Levels = await _unitOfWork.TbLevels.GetAllAsync();
            return View("Edit", model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var item = await _unitOfWork.TbModelTypes.GetFirstOrDefaultAsync(x => x.Id == id);

            if (item != null)
            {
                _unitOfWork.TbModelTypes.Delete(item);
                _unitOfWork.Complete();

                TempData["Success"] = "Delete ModelType Successfully!";

                return Json(new { success = true });
            }

            return Json(new { success = false, message = $"Not Found Model With ID: {id}" });
        }

    }
}
