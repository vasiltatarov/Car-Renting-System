namespace CarRentingSystem.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;
    using CarRentingSystem.Models;
    using CarRentingSystem.Services;

    public class HomeController : Controller
    {
        private readonly ICarService carService;

        public HomeController(ICarService carService)
            => this.carService = carService;

        public IActionResult Index()
            => View(this.carService.GetHomeCarsInfo());

        public IActionResult Privacy()
            => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
            => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
