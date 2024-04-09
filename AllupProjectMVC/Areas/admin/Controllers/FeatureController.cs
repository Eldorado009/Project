using AllupProjectMVC.Business.Interfaces;
using AllupProjectMVC.Exceptions;
using AllupProjectMVC.Exceptions.FeatureExceptions;
using AllupProjectMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AllupProjectMVC.Areas.admin.Controllers
{
    [Area("Admin")]
    public class FeatureController : MainController
    {
        private readonly IFeatureService _featureService;
        public FeatureController(IFeatureService featureService)
        {
            _featureService = featureService;
        }
        public async Task<IActionResult> Index()
           => View(await _featureService.GetAllAsync(null,"",""));
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Feature feature)
        {
            if (!ModelState.IsValid) return View();

            try
            {
                await _featureService.CreateFeature(feature);
            }
            catch (FeatureInvalidCredentialException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int Id)
        {
            Feature feature = null;
            try
            {
                feature = await _featureService.GetByIdAsync(Id);
            }
            catch (FeatureNotFoundException ex)
            {
                return View("Error");
            }
            catch (Exception ex)
            {
                throw;
            }
            return View(feature);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Feature feature)
        {
            if (!ModelState.IsValid) return View();

            try
            {
                await _featureService.CreateFeature(feature);
            }
            catch (NameAlreadyExistException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _featureService.DeleteFeature(id);
            }
            catch (FeatureNotFoundException ex)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
