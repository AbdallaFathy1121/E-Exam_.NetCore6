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
    public class ModelsController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ModelsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        [Authorize(Roles = Roles.Admin)]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = Roles.Doctor)]
        public async Task<IActionResult> ManageModels(int id)
        {
            var result = await _unitOfWork.TbModels.GetAllModelsBySubjectIdAsync(id);
            ViewBag.SubjectId = id;

            return View(result);
        }

        [Authorize(Roles = Roles.Doctor)]
        public async Task<IActionResult> Edit(int? id)
        {
            var result = await _unitOfWork.TbModels
                .GetFirstOrDefaultAsync(x => x.Id == id);

            ViewBag.ModelTypes = await _unitOfWork.TbModelTypes.GetAllAsync();
            ViewBag.Chapters = await _unitOfWork.TbChapters.GetAllAsync();
            ViewBag.SubjectId = result.SubjectId;

            if (result is not null)
            {
                ManageModelsPageVM model = new()
                {
                    SubjectId = result.SubjectId,
                    ChapterId = result.ChapterId,
                    ModelTypeId = result.ModelTypeId,
                    Id = result.Id
                };

                return View(model);
            }

            return NotFound();
        }

        [Authorize(Roles = Roles.Doctor)]
        public async Task<IActionResult> Add(int id)
        {
            ViewBag.ModelTypes = await _unitOfWork.TbModelTypes.GetAllAsync();
            ViewBag.Chapters = await _unitOfWork.TbChapters.GetAllAsync();
            ViewBag.SubjectId = id;

            return View("Edit");
        }

        [Authorize(Roles = Roles.Doctor)]
        public async Task<IActionResult> Save(ManageModelsPageVM model)
        {
            if (ModelState.IsValid)
            {
                if(model.Id == 0)
                {
                    TbModel result = new()
                    {
                        SubjectId = model.SubjectId,
                        ModelTypeId = model.ModelTypeId,
                        ChapterId = model.ChapterId
                    };

                    await _unitOfWork.TbModels.AddAsync(result);
                    _unitOfWork.Complete();

                    TempData["Success"] = "Add Model Successfully!";
                }
                else
                {
                    TbModel result = new()
                    {
                        Id = model.Id,
                        SubjectId = model.SubjectId,
                        ModelTypeId = model.ModelTypeId,
                        ChapterId = model.ChapterId
                    };

                    _unitOfWork.TbModels.Update(result);
                    _unitOfWork.Complete();

                    TempData["Success"] = "Update Model Successfully!";
                }

                return Redirect("/Admin/Models/ManageModels/" + model.SubjectId);
            }

            ViewBag.ModelTypes = await _unitOfWork.TbModelTypes.GetAllAsync();
            ViewBag.Chapters = await _unitOfWork.TbChapters.GetAllAsync();
            ViewBag.SubjectId = model.SubjectId;
            return View("Edit", model);
        }

        [Authorize(Roles = Roles.Doctor)]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _unitOfWork.TbModels.GetFirstOrDefaultAsync(x => x.Id == id);

            if (item != null)
            {
                _unitOfWork.TbModels.Delete(item);
                _unitOfWork.Complete();

                TempData["Success"] = "Delete Model Successfully!";

                return Json(new { success = true });
            }

            return Json(new { success = false, message = $"Not Found Model With ID: {id}" });
        }

    }
}
