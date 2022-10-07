using AutoMapper;
using E_Exam.Core;
using E_Exam.Core.Models;
using E_Exam.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace E_Exam.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DepartmentsController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DepartmentsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _unitOfWork.TbDepartments.GetAllAsync();

            // Using AutoMapper
            IEnumerable<DepartmentVM> model = _mapper.Map<IEnumerable<DepartmentVM>>(result);

            ViewBag.Levels = await _unitOfWork.TbLevels.GetAllAsync();

            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var result = await _unitOfWork.TbDepartments
                .GetFirstOrDefaultAsync(x => x.Id == id);

            ViewBag.Levels = await _unitOfWork.TbLevels.GetAllAsync();

            if (result == null)
            {
                return View();
            }
            else
            {
                // Using AutoMapper
                var model = _mapper.Map<DepartmentVM>(result);
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Save(DepartmentVM model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    // Using AutoMapper
                    var result = _mapper.Map<TbDepartment>(model);

                    await _unitOfWork.TbDepartments.AddAsync(result);
                    TempData["Success"] = "Add New Department Successfully!";
                }
                else
                {
                    var item = await _unitOfWork.TbDepartments.GetFirstOrDefaultAsync(x => x.Id == model.Id);
                    _unitOfWork.TbDepartments.Update(item);
                    TempData["Success"] = "Update Department Successfully!";
                }

                _unitOfWork.Complete();

                return RedirectToAction("Index");
            }

            ViewBag.Levels = await _unitOfWork.TbLevels.GetAllAsync();
            return View("Edit", model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var item = await _unitOfWork.TbDepartments.GetFirstOrDefaultAsync(x => x.Id == id);

            if (item != null)
            {
                _unitOfWork.TbDepartments.Delete(item);
                _unitOfWork.Complete();

                TempData["Success"] = "Delete Department Successfully!";

                return Json(new { success = true });
            }

            return Json(new { success = false, message = $"Not Found Department With ID: {id}" });
        }

    }
}
