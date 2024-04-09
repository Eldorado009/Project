using AllupProjectMVC.Areas.admin.ViewModels.Category;
using AllupProjectMVC.Areas.admin.ViewModels.Product;
using AllupProjectMVC.Business.Interfaces;
using AllupProjectMVC.Data;
using AllupProjectMVC.Exceptions;
using AllupProjectMVC.Exceptions.CategoryExceptions;
using AllupProjectMVC.Extension;
using AllupProjectMVC.Models;
using AutoMapper;
using Humanizer.Localisation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace AllupProjectMVC.Business.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly AllupDbContext _context;
        private readonly IWebHostEnvironment _env;
        public CategoryService(AllupDbContext context,
                                IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<List<CategoryVM>> GetAllAsync()
        {
            List<Category> categories = await _context.Categories.Include(m => m.Products).ToListAsync();

            List<CategoryVM> categoryVMs = categories.Select(category => new CategoryVM
            {
                Id = category.Id,
                Name = category.Name,
                Products = category.Products.Select(product => new ProductVM
                {
                    Id = product.Id,
                    Name = product.Name,
                }).ToList()
            }).ToList();

            return categoryVMs;
        }

        public async Task<CategoryVM> GetByIdAsync(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);
            if (category != null)
            {
                var categoryVM = new CategoryVM
                {
                    Id = category.Id,
                    Name = category.Name,
                };

                return categoryVM;
            }
            else
            {
                return null;
            }
        }

        public async Task<CategoryVM> GetByNameWithoutTrackingAsync(string name)
        {
            Category category = await _context.Categories
                               .Where(m => m.Name.Trim().ToLower() == name.Trim().ToLower())
                               .FirstOrDefaultAsync();

            if (category != null)
            {
                CategoryVM categoryVM = new CategoryVM
                {
                    Id = category.Id,
                    Name = category.Name,
                };

                return categoryVM;
            }
            else
            {
                return null;
            }
        }

        public async Task CreateAsync(CategoryCreateVM category)
        {

            string fileName = $"{Guid.NewGuid()}-{category.Photo.FileName}";

            string path = _env.GetFilePath("uploads/categories", fileName);

            await category.Photo.SaveFileAsync(path);


            var dbCategory = new Category()
            {
                Name = category.Name,
                Image = fileName
            };

            await _context.Categories.AddAsync(dbCategory);
            await _context.SaveChangesAsync();
        }



        public async Task UpdateAsync(CategoryUpdateVM category)
        {
            string fileName = $"{Guid.NewGuid()} - {category.Photo.FileName}";

            string path = _env.GetFilePath("uploads/categories", fileName);

            await category.Photo.SaveFileAsync(path);

            var categoryById = await _context.Categories.FirstOrDefaultAsync(m => m.Id == category.Id);

            if (categoryById != null)
            {
                categoryById.Image = fileName;
                categoryById.Name = category.Name;

                _context.Categories.Update(categoryById);
                await _context.SaveChangesAsync();
            }

        }


        public async Task DeleteAsync(int id)
        {
            Category dbCategory = await _context.Categories.Where(m => m.Id == id).FirstOrDefaultAsync();



            _context.Categories.Remove(dbCategory);
            await _context.SaveChangesAsync();

            string path = _env.GetFilePath("uploads/categories", dbCategory.Image);

            if (File.Exists(path))
            {
                File.Delete(path);
            }

        }

        public List<SelectListItem> GetAllSelectedAsync()
        {
            return _context.Categories.Select(m => new SelectListItem()
            {
                Text = m.Name,
                Value = m.Id.ToString(),

            }).ToList();
        }

    }
}
