namespace CarRentingSystem.Services
{
    using System.Collections.Generic;
    using CarRentingSystem.Models.Cars;
    using CarRentingSystem.Models.Home;

    public interface ICarService
    {
        void Add(string brand, string model, string imageUrl, string description, int year, int categoryId);

        IEnumerable<CarCategoryViewModel> GetCarCategories();

        AllCarsViewModel All(string searchTerms, string brand, CarSorting sorting, int currentPage);

        IndexViewModel GetHomeCarsInfo();

        CarDetailsModel GetCarDetails(int id);
    }
}
