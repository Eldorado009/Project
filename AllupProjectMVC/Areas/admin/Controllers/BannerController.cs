using AllupProjectMVC.Areas.admin.ViewModels.Banner;
using AllupProjectMVC.Business.Interfaces;
using AllupProjectMVC.Data;
using AllupProjectMVC.Extension;
using AllupProjectMVC.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace AllupProjectMVC.Areas.admin.Controllers
{
    public class BannerController : MainController
    {

        private readonly AllupDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;
        private readonly IBannerService _bannerService;
        public BannerController(AllupDbContext context,
                                    IWebHostEnvironment env,
                                    IMapper mapper,
                                    IBannerService bannerService)
        {
            _context = context;
            _env = env;
            _mapper = mapper;
            _bannerService = bannerService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _bannerService.GetAllAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            BannerVM banner = await _bannerService.GetByIdAsync((int)id);

            if (banner is null) return NotFound();

            return View(banner);
        }


        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (id is null) return BadRequest();

            Banner banner = await _context.Banners.FirstOrDefaultAsync(m => m.Id == id);

            if (banner is null) return NotFound();

            BannerUpdateVM bannerEditVM = _mapper.Map<BannerUpdateVM>(banner);


            return View(bannerEditVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, BannerUpdateVM request)
        {

            if (id is null) return BadRequest();

            Banner dbBanner = await _context.Banners.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (dbBanner is null) return NotFound();


            request.Image = dbBanner.Image;

            if (!ModelState.IsValid)
            {
                return View(request);
            }



            if (request.Photo is null)
            {
                _mapper.Map(request, dbBanner);
                _context.Banners.Update(dbBanner);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                if (!request.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File can be only image format");
                    return View(request);
                }

                if (!request.Photo.CheckFilesize(200))
                {
                    ModelState.AddModelError("Photo", "File size can  be max 200 kb");
                    return View(request);
                }
            }


            await _bannerService.UpdateAsync(request);

            return RedirectToAction(nameof(Index));
        }
    }
}
