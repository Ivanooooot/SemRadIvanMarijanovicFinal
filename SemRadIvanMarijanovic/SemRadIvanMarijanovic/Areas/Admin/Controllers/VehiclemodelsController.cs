using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SemRadIvanMarijanovic.Data;
using SemRadIvanMarijanovic.Models;

namespace SemRadIvanMarijanovic.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class VehiclemodelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VehiclemodelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Vehiclemodels
        public async Task<IActionResult> Index()
        {
            return _context.VehicleModels != null ?
                        View(await _context.VehicleModels.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.VehicleModels'  is null.");
        }

        // GET: Admin/Vehiclemodels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.VehicleModels == null)
            {
                return NotFound();
            }

            var vehiclemodel = await _context.VehicleModels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehiclemodel == null)
            {
                return NotFound();
            }

            return View(vehiclemodel);
        }

        // GET: Admin/Vehiclemodels/Create
        public IActionResult Create()
        {
            ViewBag.ErrorMessage = TempData["ErrorMessage"] as string ?? "";
            return View();
        }

        // POST: Admin/Vehiclemodels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Price,Image")] Vehiclemodel vehiclemodel,
            int[] categoryIds,
            IFormFile Image
            )
        {
            if (categoryIds.Length == 0 || categoryIds == null)
            {
                TempData["ErrorMessage"] = "Molimo odaberite minimalno jednu kategoriju!";
                return RedirectToAction(nameof(Create));
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var imageName = Image.FileName.ToLower();

                    var saveImagePath = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot/images/vehiclemodels",
                        imageName
                    );

                    Directory.CreateDirectory(Path.GetDirectoryName(saveImagePath));

                    using (
                        var stream = new FileStream(saveImagePath, FileMode.Create)
                    )
                    {
                        Image.CopyTo(stream);
                    }

                    vehiclemodel.Image = imageName;

                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = ex.Message;
                    return RedirectToAction(nameof(Create));
                }

                _context.Add(vehiclemodel);
                await _context.SaveChangesAsync();

                foreach (var categoryId in categoryIds)
                {
                    VehiclemodelCategory vehiclemodelCategory = new VehiclemodelCategory();
                    vehiclemodelCategory.CategoryId = categoryId;
                    vehiclemodelCategory.VehiclemodelId = vehiclemodel.Id;

                    _context.VehicleCategories.Add(vehiclemodelCategory);
                }
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(vehiclemodel);
        }

        // GET: Admin/Vehiclemodels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.VehicleModels == null)
            {
                return NotFound();
            }

            var vehiclemodel = await _context.VehicleModels.FindAsync(id);
            if (vehiclemodel == null)
            {
                return NotFound();
            }

            ViewBag.VehicleCategories = _context.VehicleCategories.Where(
                v => v.VehiclemodelId == vehiclemodel.Id
            ).Select(
                c => c.CategoryId
            ).ToList();

            ViewBag.ErrorMessage = TempData["ErrorMessage"] ?? "";

            return View(vehiclemodel);
        }

        // POST: Admin/Vehiclemodels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Price,Image")] Vehiclemodel vehiclemodel,
            IFormFile? newImage,
            int[] categoryIds
            )
        {
            if (id != vehiclemodel.Id)
            {
                return NotFound();
            }

            if (categoryIds.Length == 0)
            {
                TempData["ErrorMessage"] = "Choose one category!";
                return RedirectToAction(nameof(Edit), new { id = id });
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (newImage != null)
                    {
                        var newImageName = DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + "_" + newImage.FileName.ToLower().Replace(" ", "_");

                        var saveImagePath = Path.Combine(
                            Directory.GetCurrentDirectory(),
                            "wwwroot/images/vehiclemodels",
                            newImageName
                        );

                        Directory.CreateDirectory(Path.GetDirectoryName(saveImagePath));

                        using (
                            var stream = new FileStream(saveImagePath, FileMode.Create)
                        )
                        {
                            newImage.CopyTo(stream);
                        }
                        vehiclemodel.Image = newImageName;

                    }

                    _context.Update(vehiclemodel);
                    await _context.SaveChangesAsync();

                    _context.VehicleCategories.RemoveRange(
                        _context.VehicleCategories.Where(v => v.VehiclemodelId == id)
                    );
                    _context.SaveChanges();

                    foreach (var categoryId in categoryIds)
                    {
                        VehiclemodelCategory vehiclemodelCategory = new VehiclemodelCategory();
                        vehiclemodelCategory.VehiclemodelId = vehiclemodel.Id;
                        vehiclemodelCategory.CategoryId = categoryId;

                        _context.Add(vehiclemodelCategory);
                    }
                    _context.SaveChanges();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehiclemodelExists(vehiclemodel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vehiclemodel);
        }


        // GET: Admin/Vehiclemodels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.VehicleModels == null)
            {
                return NotFound();
            }

            var vehiclemodel = await _context.VehicleModels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehiclemodel == null)
            {
                return NotFound();
            }

            return View(vehiclemodel);
        }

        // POST: Admin/Vehiclemodels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.VehicleModels == null)
            {
                return Problem("Entity set 'ApplicationDbContext.VehicleModels'  is null.");
            }
            var vehiclemodel = await _context.VehicleModels.FindAsync(id);
            if (vehiclemodel != null)
            {
                _context.VehicleModels.Remove(vehiclemodel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehiclemodelExists(int id)
        {
            return (_context.VehicleModels?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
