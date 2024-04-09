using AllupProjectMVC.Areas.admin.ViewModels.Slider;
using AllupProjectMVC.Business.Interfaces;
using AllupProjectMVC.Data;
using AllupProjectMVC.Extension;
using AllupProjectMVC.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;

namespace AllupProjectMVC.Business.Implementations
{
    public class SliderService : ISliderService
    {
        private readonly AllupDbContext _context;
        private readonly IWebHostEnvironment _env;
        public SliderService(AllupDbContext context,
                              IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<List<SliderVM>> GetAllAsync()
        {
            var sliders = await _context.Sliders.ToListAsync();
            List<SliderVM> sliderVMs = new List<SliderVM>();

            foreach (var slider in sliders)
            {
                SliderVM sliderVM = new SliderVM
                {
                    Id = slider.Id,
                    Title = slider.Title,
                    Description = slider.Description,
                    Image = slider.Image
                };

                sliderVMs.Add(sliderVM);
            }

            return sliderVMs;
        }

        public async Task<SliderVM> GetByIdAsync(int id)
        {
            var slider = await _context.Sliders.FirstOrDefaultAsync(m => m.Id == id);

            if (slider == null)
            {
                return null;
            }

            var sliderVM = new SliderVM
            {
                Id = slider.Id,
                Title = slider.Title,
                Description = slider.Description,
                Image = slider.Image,
            };

            return sliderVM;
        }

        public async Task CreateAsync(SliderCreateVM slider)
        {
            string fileName = $"{Guid.NewGuid()}-{slider.Photo.FileName}";

            string path = _env.GetFilePath("uploads/sliders", fileName);

            var data = new Slider
            {
                Title = slider.Title,
                Description = slider.Description,
                Image = fileName 
            };

            await _context.AddAsync(data);
            await _context.SaveChangesAsync();

            await slider.Photo.SaveFileAsync(path);

        }

        public async Task UpdateAsync(SliderUpdateVM slider)
        {
            Slider dbSlider = await _context.Sliders.FirstOrDefaultAsync(m => m.Id == slider.Id);
            if (slider.Photo != null)
            {

                string oldPath = _env.GetFilePath("uploads/sliders", slider.Image);

                string fileName = $"{Guid.NewGuid()}-{slider.Photo.FileName}";

                string newPath = _env.GetFilePath("uploads/sliders", fileName);
                dbSlider.Image = fileName;

                if (File.Exists(oldPath))
                {
                    File.Delete(oldPath);
                }

                await slider.Photo.SaveFileAsync(newPath);

            }
            dbSlider.Title = slider.Title;
            dbSlider.Title2 = slider.Title2;
            dbSlider.Description = slider.Description;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Slider slider = await _context.Sliders.Where(m => m.Id == id).FirstOrDefaultAsync();
            _context.Sliders.Remove(slider);
            await _context.SaveChangesAsync();

            string path = _env.GetFilePath("uploads/sliders", slider.Image);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
