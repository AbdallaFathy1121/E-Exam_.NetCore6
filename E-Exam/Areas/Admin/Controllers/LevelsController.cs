using AutoMapper;
using E_Exam.Core;
using E_Exam.Core.Models;
using E_Exam.Core.ViewModels;
using E_Exam.EF;
using E_Exam.Utility.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Exam.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Roles.Admin)]
    public class LevelsController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public LevelsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            LevelPageVM model = new();

            var lstLevel = await _unitOfWork.TbLevels.GetAllAsync(new[] { "DepartmentLevels" });
            model.LstDepartmentLevel = await _unitOfWork.TbDepartmentLevels.GetAllAsync(new[] { "Department" });
            
            // using AutoMapper
            model.LstLevel = _mapper.Map<IEnumerable<LevelVM>>(lstLevel);

            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var result = await _unitOfWork.TbLevels.GetFirstOrDefaultAsync(x => x.Id == id);

            if (result == null)
            {
                return View();
            }
            else
            {
                // Using AutoMapper
                var model = _mapper.Map<LevelVM>(result);

                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Save(LevelVM model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    var item = await _unitOfWork.TbLevels.FindAsync(x => x.Name == model.Name);
                    if(item == false)
                    {
                        // Using AutoMapper
                        var result = _mapper.Map<TbLevel>(model);

                        await _unitOfWork.TbLevels.AddAsync(result);
                        TempData["Success"] = "Add New Level Successfully!";
                    }
                    else
                    {
                        TempData["Error"] = $"This Name: {model.Name} already exists";
                        return View("Edit", model);
                    }
                }
                else
                {
                    var item = await _unitOfWork.TbLevels.GetFirstOrDefaultAsync(x => x.Id == model.Id);
                    item.Name = model.Name;
                    _unitOfWork.TbLevels.Update(item);
                    TempData["Success"] = "Update Level Successfully!";
                }

                _unitOfWork.Complete();

                return RedirectToAction("Index");
            }
            return View("Edit", model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var item = await _unitOfWork.TbLevels.GetFirstOrDefaultAsync(x => x.Id == id);

            if(item != null)
            {
                _unitOfWork.TbLevels.Delete(item);
                _unitOfWork.Complete();

                TempData["Success"] = "Delete Level Successfully!";

                return Json(new { success = true });
            }

            return Json(new { success = false, message = $"Not Found Level With ID: {id}" });
        }

        public async Task<IActionResult> EditDepartmentLevel(int id)
        {
            EditDepartmentLevel model = new();

            ViewBag.LevelId = id;
            
            var departmentLevel = await _unitOfWork.TbDepartmentLevels.GetWhereAsync(x => x.LevelId == id, new[] { "Department" });
        
            var department = await _unitOfWork.TbDepartments.GetDepartmentsThatNotInDepartmentLevel(id);

            model.LstDepartmentVM = _mapper.Map<IEnumerable<DepartmentVM>>(department);
            model.LstDepartmentLevel = departmentLevel;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddDepartment(int departmentId, int levelId)
        {
            TbDepartmentLevel model = new()
            {
                LevelId = levelId,
                DepartmentId = departmentId
            };

            await _unitOfWork.TbDepartmentLevels.AddAsync(model);
            _unitOfWork.Complete();

            TempData["Success"] = "Add Department Successfully!";

            return Redirect("/Admin/Levels/EditDepartmentLevel/" + levelId);
            
        }

        [HttpPost]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var item = await _unitOfWork.TbDepartmentLevels.GetFirstOrDefaultAsync(a => a.Id == id);

            var levelId = item.LevelId;

            _unitOfWork.TbDepartmentLevels.Delete(item);
            _unitOfWork.Complete();

            TempData["Success"] = "Delete Department Successfully!";

            return Redirect("/Admin/Levels/EditDepartmentLevel/" + levelId);
        }

    }
}
