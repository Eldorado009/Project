using AllupProjectMVC.Areas.admin.ViewModels.Category;
using AllupProjectMVC.Business.Interfaces;
using AllupProjectMVC.Data;
using AllupProjectMVC.Exceptions;
using AllupProjectMVC.Exceptions.CategoryExceptions;
using AllupProjectMVC.Extension;
using AllupProjectMVC.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;

namespace AllupProjectMVC.Areas.admin.Controllers
{
    public class CategoryController : MainController
    {
        private readonly AllupDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;
        public CategoryController(AllupDbContext context,
                                    IWebHostEnvironment env,
                                    IMapper mapper,
                                    ICategoryService categoryService)
        {
            _context = context;
            _env = env;
            _mapper = mapper;
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _categoryService.GetAllAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            CategoryVM category = await _categoryService.GetByIdAsync((int)id);

            if (category is null) return NotFound();

            return View(category);
        }

        public async Task<IActionResult> Create()
        {

            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryCreateVM model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (!model.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "File size can be maximum 200 KB");
                return View(model);
            }


            if (!model.Photo.CheckFilesize(200))
            {
                ModelState.AddModelError("Photo", "File size can be maximum 200 KB");
                return View(model);
            }

            if (!model.Photo.CheckFilesize(200))
            {
                ModelState.AddModelError("Photo", "File size can be max 200 kb");
                return View(model);
            }
            string fileName = $"{Guid.NewGuid()}-{model.Photo.FileName}";

            string path = _env.GetFilePath("uploads/categories", fileName);

            await model.Photo.SaveFileAsync(path);

            var category = new Category
            {
                Name = model.Name,
                Image = fileName
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (id is null) return BadRequest();

            Category dbCategory = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (dbCategory is null) return NotFound();

           


            return View(new CategoryUpdateVM()
            {
                Name = dbCategory.Name,
                Image = dbCategory.Image

            });

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Update(int? id, CategoryUpdateVM request)
        {
            if (id is null) return BadRequest();



            Category dbCategory = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);

            if (dbCategory is null) return NotFound();


           

            if (!ModelState.IsValid)
            {
                return View(request);
            }

            CategoryVM existCategory = await _categoryService.GetByNameWithoutTrackingAsync(request.Name);


            if (request.Photo != null)
            {


                if (!request.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File can only be in image format");

                    return View(request);

                }

                if (!request.Photo.CheckFilesize(200))
                {
                    ModelState.AddModelError("Photo", "File size can be max 200 kb");
                    return View(request);
                }

                string fileName = $"{Guid.NewGuid()}-{request.Photo.FileName}";
                string path = _env.GetFilePath("uploads/categories", fileName);

                await request.Photo.SaveFileAsync(path);

                dbCategory.Image = fileName;
            }

            if (existCategory is not null && existCategory.Id != request.Id)


            if (existCategory is not null)
            {
                

                ModelState.AddModelError("Name", "This category already exists");
                return View(request);
            }

            dbCategory.Name = request.Name;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }


        [HttpPost]
        public async Task<IActionResult> DeleteAsync(int id)
        {

            await _categoryService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));

        }


    }
}
