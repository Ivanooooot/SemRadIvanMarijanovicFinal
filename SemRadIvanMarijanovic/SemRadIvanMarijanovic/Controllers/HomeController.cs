using Microsoft.AspNetCore.Mvc;
using SemRadIvanMarijanovic.Data;
using SemRadIvanMarijanovic.Models;
using System.Diagnostics;

namespace SemRadIvanMarijanovic.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Index(string? searchQuery, int? categoryId = 0)
        {


            List<Vehiclemodel> vehiclemodels = _dbContext.VehicleModels.ToList();

            Random random = new Random();

            if (categoryId > 0)
            {
                vehiclemodels = vehiclemodels.Where(
                    v => _dbContext.VehicleCategories.Where(
                            vc => vc.CategoryId == categoryId
                        ).Select(
                            vc => vc.VehiclemodelId
                        ).ToList().Contains(
                            v.Id
                        )
                ).ToList();
            }

            if (!String.IsNullOrWhiteSpace(searchQuery))
            {
                vehiclemodels = vehiclemodels.Where(p => p.Title.ToLower().Contains(searchQuery.ToLower())).ToList();
            }

            ViewBag.Categories = _dbContext.Categories.ToList();

            ViewBag.ThankYouMessage = TempData["ThankYouMessage"] as string ?? "";


            List<Vehiclemodel> randomVehiclemodels = vehiclemodels.OrderBy(v => random.Next()).Take(10).ToList();

            return View(randomVehiclemodels);
        }

        public IActionResult Categories(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Index");
            }

            List<Vehiclemodel> vehicles = new List<Vehiclemodel>();

            vehicles = _dbContext.VehicleModels.Where(
                    p => _dbContext.VehicleCategories.Where(
                            pc => pc.CategoryId == id
                        ).Select(
                            pc => pc.VehiclemodelId
                        ).ToList().Contains(
                            p.Id
                        )
                ).ToList();

            return View(vehicles);

        }


        public IActionResult Privacy()
        {

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}