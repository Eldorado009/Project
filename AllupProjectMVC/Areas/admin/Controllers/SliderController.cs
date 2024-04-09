using AllupProjectMVC.Areas.admin.ViewModels.Slider;
using AllupProjectMVC.Business.Interfaces;
using AllupProjectMVC.Data;
using AllupProjectMVC.Extension;
using AllupProjectMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace AllupProjectMVC.Areas.admin.Controllers
{
    public class SliderController : MainController
    {
        private readonly AllupDbContext _context;
        private readonly ISliderService _sliderService;
        private readonly IWebHostEnvironment _env;


        public SliderController(AllupDbContext context,
                                ISliderService sliderService,
                                IWebHostEnvironment env)
        {
            _context = context;
            _sliderService = sliderService;
            _env = env;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _sliderService.GetAllAsync());
        }


        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            SliderVM slider = await _sliderService.GetByIdAsync((int)id);

            if (slider is null) return NotFound();

            return View(slider);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SliderCreateVM slider)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }


            if (!slider.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photos", "File can be only image format");
                return View();
            }

            if (!slider.Photo.CheckFilesize(200))
            {
                ModelState.AddModelError("Photos", "File size can be max 200 kb");
                return View();
            }



            await _sliderService.CreateAsync(slider);


            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (id is null) return BadRequest();

            SliderVM slider = await _sliderService.GetByIdAsync((int)id);

            if (slider is null) return NotFound();

            return View(new SliderUpdateVM { Image = slider.Image, Title = slider.Title, Description = slider.Description, Title2 = slider.Title2 });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(SliderUpdateVM slider, int? id)
        {
            if (id is null) return BadRequest();
            SliderVM dbSlider = await _sliderService.GetByIdAsync((int)id);
            if (dbSlider is null) return NotFound();
            slider.Image = dbSlider.Image;

            if (!ModelState.IsValid)
            {
                return View(slider);
            }
            if (slider.Photo is not null)
            {
                if (!slider.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File can be only image format");
                    return View(slider);
                }
                if (!slider.Photo.CheckFilesize(200))
                {
                    ModelState.AddModelError("Photo", "File size can be max 200 kb");
                    return View(slider);
                }
            }
            await _sliderService.UpdateAsync(slider);
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _sliderService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
