using AllupProjectMVC.Areas.admin.ViewModels.Category;
using AllupProjectMVC.Areas.admin.ViewModels.Product;
using AllupProjectMVC.Business.Interfaces;
using AllupProjectMVC.Data;
using AllupProjectMVC.Exceptions.ProductExceptions;
using AllupProjectMVC.Extension;
using AllupProjectMVC.Models;
using AutoMapper;
using Humanizer.Localisation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using static System.Net.Mime.MediaTypeNames;

namespace AllupProjectMVC.Business.Implementations
{
    public class ProductService : IProductService
    {
        private readonly AllupDbContext _context;
        private readonly IWebHostEnvironment _env;
        public ProductService(AllupDbContext context,
                              IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<List<ProductVM>> GetAllAsync()
        {
            var products = await _context.Products
                          .Include(m => m.Category)
                          .ToListAsync();

            List<ProductVM> productVMs = new List<ProductVM>();

            foreach (var product in products)
            {
                ProductVM productVM = new ProductVM
                {
                    Id = product.Id,
                    Name = product.Name,
                    CategoryName = product.Category.Name  
                };

                productVMs.Add(productVM);
            }

            return productVMs;


        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Products.CountAsync();
        }

        public async Task<List<ProductVM>> GetPaginatedDatasAsync(int page, int take)
        {
            List<Product> products = await _context.Products
                                    .Include(m => m.Category)
                                    .Skip((page * take) - take)
                                    .Take(take)
                                    .ToListAsync();

            List<ProductVM> productVMs = new List<ProductVM>();

            foreach (var product in products)
            {
                ProductVM productVM = new ProductVM
                {
                    Id = product.Id,
                    Name = product.Name,
                    CategoryName = product.Category.Name,
                    Images = product.Images                };

                productVMs.Add(productVM);
            }

            return productVMs;

        }

        public async Task<Product> GetByIdWithIncludesAsync(int id)
        {
            Product data = await _context.Products.Include(m => m.Category)
                                                   .Include(m => m.Images)
                                                   .FirstOrDefaultAsync(m => m.Id == id);

            return data;
        }

        public async Task<Product> GetProductDatasModalAsync(int id)
        {
            var data = await _context.Products
          .Include(m => m.Images)
          .Include(m => m.Category)
          .FirstOrDefaultAsync(m => m.Id == id);

            return data;
        }


        public async Task<List<ProductVM>> GetProductsByCategoryAsync(int id, int page = 1, int take = 6)
        {
            List<ProductVM> model = new();

            var products = await _context.Products
                         .Where(m => m.CategoryId == id)
                         .Include(m => m.Images)
                         .Include(m => m.Category)
                         .Skip((page * take) - take)
                         .Take(take)
                         .ToListAsync();




            foreach (var product in products)
            {
                model.Add(new ProductVM
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Images = product.Images,

                });
            }

            return model;

        }


        

        public async Task<int> GetCountByCategoryAsync(int id)
        {
            return await _context.Products.Where(m => m.CategoryId == id)
                                           .Include(m => m.Category)
                                           .Include(m => m.Images)
                                           .CountAsync();
        }


        public async Task DeleteAsync(int id)
        {
            Product dbproduct = await _context.Products.Include(m => m.Images).FirstOrDefaultAsync(m => m.Id == id);


            _context.Products.Remove(dbproduct);
            await _context.SaveChangesAsync();


            foreach (var photo in dbproduct.Images)
            {

                string path = _env.GetFilePath("uploads/products", photo.Image);

                if (File.Exists(path))
                {
                    File.Delete(path);
                }


            }
        }


        public async Task<List<ProductVM>> GetPaginatedDatasByCategory(int id, int page, int take)
        {
            List<Product> products = await _context.Products
                                    .Where(m => m.CategoryId == id)
                                    .Include(m => m.Category)
                                    .Skip((page * take) - take)
                                    .Take(take)
                                    .ToListAsync();

            List<ProductVM> productVMs = new List<ProductVM>();

            foreach (var product in products)
            {
                ProductVM productVM = new ProductVM
                {
                    Id = product.Id,
                    Name = product.Name,
                    CategoryName = product.Category.Name,
                    Images = product.Images
                };

                productVMs.Add(productVM);
            }

            return productVMs;


        }



        public async Task<List<ProductVM>> OrderByPriceAsc(int page, int take)
        {
            var dbProducts = await _context.Products
                .OrderBy(p => p.Price)
                .Skip((page * take) - take)
                .Take(take)
                .ToListAsync();

            List<ProductVM> productVMs = new List<ProductVM>();

            foreach (var product in dbProducts)
            {
                ProductVM productVM = new ProductVM
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Images = product.Images,
                };

                productVMs.Add(productVM);
            }

            return productVMs;
        }

        public async Task<List<ProductVM>> OrderByPriceDesc(int page, int take)
        {
            var dbProducts = await _context.Products
                .Include(m => m.Images)
                .OrderByDescending(p => p.Price)
                .Skip((page * take) - take)
                .Take(take)
                .ToListAsync();

            List<ProductVM> productVMs = new List<ProductVM>();

            foreach (var product in dbProducts)
            {
                ProductVM productVM = new ProductVM
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Images = product.Images
                };

                productVMs.Add(productVM);
            }

            return productVMs;
        }

        public async Task<List<ProductVM>> OrderByLatestDate(int page, int take)
        {
            var dbProducts = await _context.Products
                .Include(m => m.Images)
                .OrderByDescending(p => p.Id)
                .Skip((page * take) - take)
                .Take(take)
                .ToListAsync();

            List<ProductVM> productVMs = new List<ProductVM>();

            foreach (var product in dbProducts)
            {
                ProductVM productVM = new ProductVM
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Images = product.Images
                };

                productVMs.Add(productVM);
            }

            return productVMs;
        }


        public async Task<int> GetCountBySearch(string searchText)
        {
            return await _context.Products.Include(m => m.Images)
                                                 .Include(m => m.Category)
                                                 .OrderByDescending(m => m.Id)
                                                 .Where(m => m.Name.ToLower().Trim().Contains(searchText.ToLower().Trim()))
                                                 .CountAsync();

        }


        public async Task<List<ProductVM>> SearchAsync(string searchText, int page, int take)
        {
            var dbProducts = await _context.Products
                .Include(m => m.Images)
                .Include(m => m.Category)
                .OrderByDescending(m => m.Id)
                .Where(m => m.Name.ToLower().Trim().Contains(searchText.ToLower().Trim()))
                .Skip((page * take) - take)
                .Take(take)
                .ToListAsync();

            List<ProductVM> productVMs = new List<ProductVM>();

            foreach (var product in dbProducts)
            {
                ProductVM productVM = new ProductVM
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    CategoryName = product.Category?.Name,
                    Images = product.Images
                };

                productVMs.Add(productVM);
            }

            return productVMs;
        }


    }
}
