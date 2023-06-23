using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SemRadIvanMarijanovic.Data;
using SemRadIvanMarijanovic.Models;

namespace SRAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclemodelsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public VehiclemodelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Vehiclemodels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vehiclemodel>>> GetVehicleModels()
        {
            if (_context.VehicleModels == null)
            {
                return NotFound();
            }
            return await _context.VehicleModels.ToListAsync();
        }

        // GET: api/Vehiclemodels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vehiclemodel>> GetVehiclemodel(int id)
        {
            if (_context.VehicleModels == null)
            {
                return NotFound();
            }
            var vehiclemodel = await _context.VehicleModels.FindAsync(id);

            if (vehiclemodel == null)
            {
                return NotFound();
            }

            return vehiclemodel;
        }

        // PUT: api/Vehiclemodels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehiclemodel(int id, Vehiclemodel vehiclemodel)
        {
            if (id != vehiclemodel.Id)
            {
                return BadRequest();
            }

            _context.Entry(vehiclemodel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehiclemodelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Vehiclemodels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Vehiclemodel>> PostVehiclemodel(Vehiclemodel vehiclemodel)
        {
            if (_context.VehicleModels == null)
            {
                return Problem("Entity set 'ApplicationDbContext.VehicleModels'  is null.");
            }
            _context.VehicleModels.Add(vehiclemodel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVehiclemodel", new { id = vehiclemodel.Id }, vehiclemodel);
        }

        // DELETE: api/Vehiclemodels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehiclemodel(int id)
        {
            if (_context.VehicleModels == null)
            {
                return NotFound();
            }
            var vehiclemodel = await _context.VehicleModels.FindAsync(id);
            if (vehiclemodel == null)
            {
                return NotFound();
            }

            _context.VehicleModels.Remove(vehiclemodel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VehiclemodelExists(int id)
        {
            return (_context.VehicleModels?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
