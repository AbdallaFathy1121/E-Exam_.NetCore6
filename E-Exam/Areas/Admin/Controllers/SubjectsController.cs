using AutoMapper;
using E_Exam.Core;
using E_Exam.Core.Models;
using E_Exam.Core.ViewModels;
using E_Exam.Utility.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Exam.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SubjectsController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        public SubjectsController(IUnitOfWork unitOfWork, IMapper mapper, 
            UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Index()
        {
            SubjectPageVM model = new();

            var subjects = await _unitOfWork.TbSubjects.GetAllAsync(new[] {"Level", "User" });
            model.LstSubjectDepartments = await _unitOfWork.TbSubjectDepartments.GetAllAsync(new[] { "Department" });

            // using AutoMapper
            model.LstSubjects = _mapper.Map<IEnumerable<SubjectVM>>(subjects);

            return View(model);
        }

        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Edit(int? id)
        {
            var result = await _unitOfWork.TbSubjects.GetFirstOrDefaultAsync(x => x.Id == id);

            ViewBag.Levels = await _unitOfWork.TbLevels.GetAllAsync();
            ViewBag.Users = await _userManager.Users.Where(a => a.Level == null && a.Email != "admin@admin.com").Select(a => new
            {
                a.Id,
                a.FullName
            }).ToListAsync();

            if (result == null)
            {
                return View();
            }
            else
            {
                // Using AutoMapper
                var model = _mapper.Map<SubjectVM>(result);

                return View(model);
            }
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPost]
        public async Task<IActionResult> Save(SubjectVM model)
        {
            if (ModelState.IsValid)
            {
                if(model.Id == 0)
                {
                    var item = await _unitOfWork.TbSubjects.FindAsync(x => x.Name == model.Name);
                    if (item == false)
                    {
                        var result = _mapper.Map<TbSubject>(model);
                        await _unitOfWork.TbSubjects.AddAsync(result);

                        TempData["Success"] = "Add New Subject Successfully!";
                    }
                    else
                    {
                        TempData["Error"] = $"This Name: {model.Name} already exists";

                        ViewBag.Levels = await _unitOfWork.TbLevels.GetAllAsync();
                        ViewBag.Users = await _userManager.Users.Where(a => a.Level == null && a.Email != "admin@admin.com").Select(a => new
                        {
                            a.Id,
                            a.FullName
                        }).ToListAsync();

                        return View("Edit", model);
                    }
                }
                else
                {
                    var item = await _unitOfWork.TbSubjects.GetFirstOrDefaultAsync(x => x.Id == model.Id);
                    item.Name = model.Name;
                    item.UserId = model.UserId;
                    item.LevelId = model.LevelId;

                    _unitOfWork.TbSubjects.Update(item);
                    TempData["Success"] = "Update Subject Successfully!";
                }

                _unitOfWork.Complete();

                return RedirectToAction("Index");
            }

            ViewBag.Levels = await _unitOfWork.TbLevels.GetAllAsync();
            ViewBag.Users = await _userManager.Users.Where(a => a.Level == null && a.Email != "admin@admin.com").Select(a => new
            {
                a.Id,
                a.FullName
            }).ToListAsync();

            return View("Edit", model);
        }

        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _unitOfWork.TbSubjects.GetFirstOrDefaultAsync(a => a.Id == id);
            if(item is not null)
            {
                _unitOfWork.TbSubjects.Delete(item);
                _unitOfWork.Complete();
                TempData["Success"] = "Delete Subject Successfully";

                return Json(new { success = true });
            }

            return Json(new { success = false, message = $"Not Found Subject With ID: {id}" });
        }

        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> EditDepartmentSubject(string id)
        {
            var arr = id.Split("--");
            int subjectId = int.Parse(arr[0]);
            int levelId = int.Parse(arr[1]);

            ViewBag.SubjectId = subjectId;
            ViewBag.LevelId = levelId;

            var departments = await _unitOfWork.TbDepartments.GetDepartmentsThatNotInDepartmentSubject(subjectId, levelId);

            EditDepartmentSubjectVM model = new();
            model.LstDepartments = _mapper.Map<IEnumerable<DepartmentVM>>(departments);
            model.LstSubjectDepartments = await _unitOfWork.TbSubjectDepartments
                .GetWhereAsync(a => a.SubjectId == subjectId, new[] { "Department" });

            return View(model);
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPost]
        public async Task<IActionResult> AddDepartment(int departmentId, int subjectId, int levelId)
        {
            TbSubjectDepartment model = new()
            {
                SubjectId = subjectId,
                DepartmentId = departmentId,
            };

            string id = $"{subjectId}--{levelId}";

            await _unitOfWork.TbSubjectDepartments.AddAsync(model);
            _unitOfWork.Complete();

            TempData["Success"] = "Add Department Successfully!";

            return Redirect("/Admin/Subjects/EditDepartmentSubject/" + id);
        }

        [Authorize(Roles = Roles.Doctor)]
        // Get Subjects For Doctor By Email
        public async Task<IActionResult> List(string id)
        {
            SubjectPageVM model = new();

            model.LstSubjectDepartments = await _unitOfWork.TbSubjectDepartments.GetAllAsync(new[] { "Department" });
            var subjects = await _unitOfWork.TbSubjects.GetWhereAsync(a => a.User.Email == id, new[] { "Level" });
            
            // Using AutoMapper
            model.LstSubjects = _mapper.Map<IEnumerable<SubjectVM>>(subjects);

            return View(model);
        }



    }
}
