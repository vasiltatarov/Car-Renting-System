namespace CarRentingSystem.Controllers
{
    using System.Linq;
    using System.Collections.Generic;

    using CarRentingSystem.Data;
    using CarRentingSystem.Data.Models;
    using CarRentingSystem.Models.Cars;

    using Microsoft.AspNetCore.Mvc;

    public class CarsController : Controller
    {
        private readonly CarRentingDbContext data;

        public CarsController(CarRentingDbContext data)
        {
            this.data = data;
        }

        public IActionResult Add()
            => this.View(new AddCarFormModel { Categories = this.GetCarCategories() });

        [HttpPost]
        public IActionResult Add(AddCarFormModel car)
        {
            if (!this.data.Categories.Any(x => x.Id == car.CategoryId))
            {
                this.ModelState.AddModelError(nameof(car.CategoryId), "Category does not exits.");
            }

            if (!this.ModelState.IsValid)
            {
                car.Categories = this.GetCarCategories();
                return this.View(car);
            }

            var carData = new Car
            {
                Brand = car.Brand,
                Description = car.Description,
                ImageUrl = car.ImageUrl,
                Model = car.Model,
                Year = car.Year,
                CategoryId = car.CategoryId,
            };

            this.data.Cars.Add(carData);
            this.data.SaveChanges();

            return this.RedirectToAction("Index", "Home");
        }

        private IEnumerable<CarCategoryViewModel> GetCarCategories()
            => this.data.Categories
                .Select(x => new CarCategoryViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .ToList();
    }
}
