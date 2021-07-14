namespace CarRentingSystem.Infrastructure
{
    using System.Linq;

    using CarRentingSystem.Data;
    using CarRentingSystem.Data.Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Builder;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var data = scopedServices.ServiceProvider.GetService<CarRentingDbContext>();
            data.Database.Migrate();

            SeedCategories(data);
            SeedCars(data);

            return app;
        }

        private static void SeedCars(CarRentingDbContext data)
        {
            if (data.Cars.Any())
            {
                return;
            }

            data.Cars.AddRange(new []
            {
                new Car
                {
                    Brand = "BMW",
                    Model = "F10",
                    Description = "This car is amazing",
                    Year = 2012,
                    ImageUrl = "http://www.carhire.bg/wp-content/uploads/car-rental-gallery/1467381203_BMW_530d_F10_front.jpg",
                    CategoryId = 3,
                },
                new Car
                {
                    Brand = "Mercedes-Benz",
                    Model = "GLE",
                    Description = "This car is amazing",
                    Year = 2021,
                    ImageUrl = "https://i.gaw.to/vehicles/photos/40/23/402337-2021-mercedes-benz-gle.jpg",
                    CategoryId = 7,
                },
                new Car
                {
                    Brand = "Mercedes-Benz",
                    Model = "S63 Coupe",
                    Description = "This car is amazing",
                    Year = 2020,
                    ImageUrl = "https://www.alainclass.com/wp-content/uploads/2020/05/2018-Mercedes-Benz-S63-Coupe-Brabus-GREY-34100-3.jpg",
                    CategoryId = 7,
                },
                new Car
                {
                    Brand = "Opel",
                    Model = "Zafira",
                    Description = "This car is amazing",
                    Year = 2007,
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/7/7f/Opel_Zafira_A_Facelift_front_20091022.jpg/275px-Opel_Zafira_A_Facelift_front_20091022.jpg",
                    CategoryId = 6,
                },
                new Car
                {
                    Brand = "Peugeot",
                    Model = "307",
                    Description = "This car is amazing",
                    Year = 2003,
                    ImageUrl = "https://lh3.googleusercontent.com/proxy/lihkx1EZC_EsLpqFIeC2xlbH7qtJWOtpNCZZAY9Lxix1HDAsFddQ42HJeUqH6eb72zmgGliOOkkN8zM90zZZ",
                    CategoryId = 3,
                },
                new Car
                {
                    Brand = "Mercedes-Benz",
                    Model = "C220 CDI",
                    Description = "This car is amazing",
                    Year = 2009,
                    ImageUrl = "https://automedia.investor.bg/media/files/old/upload/news/body/mecc24_4add5f67b7248.jpg",
                    CategoryId = 3,
                },
            });

            data.SaveChanges();
        }

        private static void SeedCategories(CarRentingDbContext data)
        {
            if (data.Categories.Any())
            {
                return;
            }

            data.Categories.AddRange(new[]
            {
                new Category { Name = "Mini" },
                new Category { Name = "Economy" },
                new Category { Name = "Midsize" },
                new Category { Name = "Large" },
                new Category { Name = "SUV" },
                new Category { Name = "Vans" },
                new Category { Name = "Luxury" },
            });

            data.SaveChanges();
        }
    }
}
