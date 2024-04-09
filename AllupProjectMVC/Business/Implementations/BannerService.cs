using AllupProjectMVC.Areas.admin.ViewModels.Banner;
using AllupProjectMVC.Areas.admin.ViewModels.Blog;
using AllupProjectMVC.Business.Interfaces;
using AllupProjectMVC.Data;
using AllupProjectMVC.Extension;
using AllupProjectMVC.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace AllupProjectMVC.Business.Implementations
{
    public class BannerService : IBannerService
    {
        private readonly AllupDbContext _context;
        private readonly IWebHostEnvironment _env;
        public BannerService(AllupDbContext context,
                             IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<List<Banner>> GetAllAsync(Expression<Func<Banner, bool>>? expression = null, params string[] includes)
        {
            IQueryable<Banner> query = _context.Banners;

            foreach (string include in includes)
            {
                query = query.Include(include);
            }

            if (expression != null)
            {
                query = query.Where(expression);
            }

            List<Banner> banners = await query.ToListAsync();

            return banners;
        }
    

        public async Task<BannerVM> GetByIdAsync(int id)
        {
            Banner banner = await _context.Banners.FirstOrDefaultAsync(m => m.Id == id);
            if (banner != null)
            {
                BannerVM bannerViewModel = new BannerVM
                {
                    Id = banner.Id,
                };

                return bannerViewModel;
            }
            else
            {
                return null;
            }
        }

    

        public async Task UpdateAsync(BannerUpdateVM request)
        {
            string oldPath = _env.GetFilePath("uploads/banners", request.Image);

            string fileName = $"{Guid.NewGuid()} - {request.Photo.FileName}";

            string newPath = _env.GetFilePath("uploads/banners", fileName);

            Banner dbBanner = await _context.Banners.AsNoTracking().FirstOrDefaultAsync(m => m.Id == request.Id);



            dbBanner.Image = fileName;

            _context.Banners.Update(dbBanner);
            await _context.SaveChangesAsync();



            if (File.Exists(oldPath))
            {
                File.Delete(oldPath);
            }

            await request.Photo.SaveFileAsync(newPath);
        }

        private IQueryable<Feature> _getIncludes(IQueryable<Feature> query, params string[] includes)
        {
            if (includes is not null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return query;
        }
    }
}
