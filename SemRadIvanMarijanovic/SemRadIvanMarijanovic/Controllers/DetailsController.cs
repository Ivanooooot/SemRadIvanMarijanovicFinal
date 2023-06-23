using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SemRadIvanMarijanovic.Data;
using SemRadIvanMarijanovic.Models;

namespace SemradImarijanovic.Controllers
{
    public class DetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Details
        public async Task<IActionResult> Index()
        {
              return _context.VehicleModels != null ? 
                          View(await _context.VehicleModels.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.VehicleModels'  is null.");
        }

        // GET: Details/Details/5
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

        // GET: Details/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Details/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Price,Image")] Vehiclemodel vehiclemodel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vehiclemodel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vehiclemodel);
        }

        // GET: Details/Edit/5
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
            return View(vehiclemodel);
        }

        // POST: Details/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Price,Image")] Vehiclemodel vehiclemodel)
        {
            if (id != vehiclemodel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehiclemodel);
                    await _context.SaveChangesAsync();
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

        // GET: Details/Delete/5
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

        // POST: Details/Delete/5
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
