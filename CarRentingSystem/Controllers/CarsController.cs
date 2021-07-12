namespace CarRentingSystem.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using CarRentingSystem.Services;
    using CarRentingSystem.Models.Cars;

    public class CarsController : Controller
    {
        private readonly ICarService carService;
        private readonly ICategoryService categoryService;

        public CarsController(ICarService carService, ICategoryService categoryService)
        {
            this.carService = carService;
            this.categoryService = categoryService;
        }

        public IActionResult Add()
            => this.View(new AddCarFormModel { Categories = this.carService.GetCarCategories() });

        [HttpPost]
        public IActionResult Add(AddCarFormModel car)
        {
            if (!this.categoryService.IsCategoryExist(car.CategoryId))
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
