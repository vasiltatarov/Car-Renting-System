namespace CarRentingSystem.Services
{
    using System.Linq;
    using System.Collections.Generic;

    using CarRentingSystem.Data;
    using CarRentingSystem.Data.Models;
    using CarRentingSystem.Models.Cars;

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

        public IEnumerable<CarViewModel> GetAll()
            => this.data.Cars
                .Select(x => new CarViewModel
                {
                    Id = x.Id,
                    Brand = x.Brand,
                    Category = x.Category.Name,
                    Description = x.Description,
                    ImageUrl = x.ImageUrl,
                    Model = x.Model,
                    Year = x.Year,
                })
                .ToList();
    }
}
