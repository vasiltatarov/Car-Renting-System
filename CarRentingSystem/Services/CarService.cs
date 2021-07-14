namespace CarRentingSystem.Services
{
    using System.Linq;
    using System.Collections.Generic;

    using CarRentingSystem.Data;
    using CarRentingSystem.Data.Models;
    using CarRentingSystem.Models.Cars;
    using CarRentingSystem.Models.Home;

    public class CarService : ICarService
    {
        private readonly CarRentingDbContext data;

        public CarService(CarRentingDbContext data) => this.data = data;

        public void Add(string brand, string model, string imageUrl, string description, int year, int categoryId)
        {
            var carData = new Car
            {
                Brand = brand,
                Description = description,
                ImageUrl = imageUrl,
                Model = model,
                Year = year,
                CategoryId = categoryId,
            };

            this.data.Cars.Add(carData);
            this.data.SaveChanges();
        }

        public IEnumerable<CarCategoryViewModel> GetCarCategories()
            => this.data.Categories
            .Select(x => new CarCategoryViewModel
            {
                Id = x.Id,
                Name = x.Name,
            })
            .ToList();

        public AllCarsViewModel All(string searchTerms, string brand, CarSorting sorting, int currentPage)
        {
            var carsQuery = this.data.Cars.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerms))
            {
                carsQuery = carsQuery
                    .Where(x =>
                        (x.Brand + " " + x.Model).ToLower().Contains(searchTerms.ToLower()) ||
                        x.Description.ToLower().Contains(searchTerms.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(brand))
            {
                carsQuery = carsQuery
                    .Where(x => x.Brand.ToLower() == brand.ToLower());
            }

            carsQuery = sorting switch
            {
                CarSorting.Year => carsQuery.OrderByDescending(x => x.Year),
                CarSorting.BrandAndModel => carsQuery.OrderBy(x => x.Brand).ThenBy(x => x.Model),
                _ or CarSorting.DateCreated => carsQuery.OrderByDescending(x => x.Id),
            };

            var cars = carsQuery
                .Skip((currentPage - 1) * AllCarsViewModel.CarsPerPage)
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

            var totalCars = carsQuery.Count();

            return new AllCarsViewModel
            {
                Cars = cars,
                Brands = brands,
                Brand = brand,
                SearchTerms = searchTerms,
                Sorting = sorting,
                CurrentPage = currentPage,
                TotalCars = totalCars,
            };
        }

        public IndexViewModel GetHomeCarsInfo()
            => new IndexViewModel
            {
                TotalCars = this.data.Cars.Count(),
                TotalRents = 0,
                TotalUsers = 0,
                Cars = this.data
                    .Cars
                    .OrderByDescending(x => x.Id)
                    .Select(x => new CarIndexViewModel
                    {
                        Id = x.Id,
                        Brand = x.Brand,
                        ImageUrl = x.ImageUrl,
                        Model = x.Model,
                        Year = x.Year,
                    })
                    .Take(3)
                    .ToList(),
            };

        public CarDetailsModel GetCarDetails(int id)
            => this.data.Cars
                .Where(x => x.Id == id)
                .Select(x => new CarDetailsModel
                {
                    Id = x.Id,
                    Brand = x.Brand,
                    Category = x.Category.Name,
                    ImageUrl = x.ImageUrl,
                    Model = x.Model,
                    Year = x.Year,
                    Description = x.Description,
                })
                .FirstOrDefault();
    }
}
