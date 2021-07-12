namespace CarRentingSystem.Services
{
    using System.Linq;
    using CarRentingSystem.Data;

    public class CategoryService : ICategoryService
    {
        private readonly CarRentingDbContext data;

        public CategoryService(CarRentingDbContext data) => this.data = data;

        public bool IsCategoryExist(int id)
            => this.data.Categories.Any(x => x.Id == id);
    }
}
