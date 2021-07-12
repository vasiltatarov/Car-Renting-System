namespace CarRentingSystem.Controllers
{
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;

    using CarRentingSystem.Services;
    using CarRentingSystem.Data;
    using CarRentingSystem.Models.Cars;

    public class CarsController : Controller
    {
        private readonly ICarService carService;
        private readonly CarRentingDbContext data;

        public CarsController(ICarService carService, CarRentingDbContext data)
        {
            this.carService = carService;
        }

        public IActionResult Add()
            => this.View(new AddCarFormModel { Categories = this.carService.GetCarCategories() });

        [HttpPost]
        public IActionResult Add(AddCarFormModel car)
        {
            if (!this.data.Categories.Any(x => x.Id == car.CategoryId))
            {
                this.ModelState.AddModelError(nameof(car.CategoryId), "Category does not exits.");
            }

            if (!this.ModelState.IsValid)
            {
                car.Categories = this.carService.GetCarCategories();
                return this.View(car);
            }

            this.carService.Add(car.Brand, car.Model, car.ImageUrl, car.Description, car.Year, car.CategoryId);

            return this.RedirectToAction("Index", "Home");
        }

        public IActionResult All()
            => this.View(this.carService.GetAll());
    }
}
