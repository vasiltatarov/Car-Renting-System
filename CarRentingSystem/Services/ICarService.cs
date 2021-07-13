namespace CarRentingSystem.Services
{
    using System.Collections.Generic;
    using CarRentingSystem.Models.Cars;

    public interface ICarService
    {
        void Add(string brand, string model, string imageUrl, string description, int year, int categoryId);

        IEnumerable<CarCategoryViewModel> GetCarCategories();

        IEnumerable<CarViewModel> GetAll();

        CarDetailsModel GetCarDetails(int id);
    }
}
