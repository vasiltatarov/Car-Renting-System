namespace CarRentingSystem.Models.Cars
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AllCarsViewModel
    {
        public string Brand { get; set; }

        public IEnumerable<string> Brands { get; set; }

        [Display(Name = "Search by text")]
        public string SearchTerms { get; set; }

        public CarSorting Sorting { get; set; }

        public IEnumerable<CarViewModel> Cars { get; set; }
    }
}
