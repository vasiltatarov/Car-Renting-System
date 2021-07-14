namespace CarRentingSystem.Controllers
{
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;

    using CarRentingSystem.Data;
    using CarRentingSystem.Services;
    using CarRentingSystem.Models.Cars;

    public class CarsController : Controller
    {
        private readonly ICarService carService;
        private readonly ICategoryService categoryService;
        private readonly CarRentingDbContext data;

        public CarsController(ICarService carService, ICategoryService categoryService, CarRentingDbContext data)
        {
            this.carService = carService;
            this.categoryService = categoryService;
            this.data = data;
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

        public IActionResult All([FromQuery]AllCarsViewModel query)
        {
            var carsQuery = this.data.Cars.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.SearchTerms))
            {
                carsQuery = carsQuery
                    .Where(x =>
                        (x.Brand + " " + x.Model).ToLower().Contains(query.SearchTerms.ToLower()) ||
                        x.Description.ToLower().Contains(query.SearchTerms.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(query.Brand))
            {
                carsQuery = carsQuery
                    .Where(x => x.Brand.ToLower() == query.Brand.ToLower());
            }

            carsQuery = query.Sorting switch
            {
                CarSorting.Year => carsQuery.OrderByDescending(x => x.Year),
                CarSorting.BrandAndModel => carsQuery.OrderBy(x => x.Brand).ThenBy(x => x.Model),
                _ or CarSorting.DateCreated => carsQuery.OrderByDescending(x => x.Id),
            };

            var cars = carsQuery
                .Skip((query.CurrentPage - 1) * AllCarsViewModel.CarsPerPage)
                .Take(AllCarsViewModel.CarsPerPage)
                .Select(x => new CarViewModel
                {
                    Id = x.Id,
                    Brand = x.Brand,
                    Category = x.Category.Name,
                    ImageUrl = x.ImageUrl,
                    Model = x.Model,
                    Year = x.Year,
                });

            var brands = this.data.Cars
                .Select(x => x.Brand)
                .Distinct()
                .OrderBy(x => x)
                .ToList();

            var totalCars = this.data.Cars.Count();

            return this.View(new AllCarsViewModel
            {
                Cars = cars,
                Brands = brands,
                Brand = query.Brand,
                SearchTerms = query.SearchTerms,
                Sorting = query.Sorting,
                CurrentPage = query.CurrentPage,
                TotalCars = totalCars,
            });
        }

        public IActionResult Details(int id)
            => this.View(this.carService.GetCarDetails(id));
    }
}
