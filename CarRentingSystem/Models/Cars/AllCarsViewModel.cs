namespace CarRentingSystem.Models.Cars
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AllCarsViewModel
    {
        public const int CarsPerPage = 2;

        public string Brand { get; set; }

        [Display(Name = "Search by text")]
        public string SearchTerms { get; set; }

        public CarSorting Sorting { get; set; }

        public int CurrentPage { get; set; } = 1;

        public int TotalCars { get; set; }

        public IEnumerable<string> Brands { get; set; }

        public IEnumerable<CarViewModel> Cars { get; set; }
    }
}
