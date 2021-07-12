using System.Collections.Generic;
using CarRentingSystem.Models.Cars;

namespace CarRentingSystem.Services
{
    public interface ICarService
    {
        void Add(string brand, string model, string imageUrl, string description, int year, int categoryId);

        IEnumerable<CarCategoryViewModel> GetCarCategories();

        IEnumerable<CarViewModel> GetAll();
    }
}
