namespace CarRentingSystem.Models.Cars
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class AddCarFormModel
    {
        [Required]
        [StringLength(
            CarBrandMaxLength,
            MinimumLength = CarBrandMinLength,
            ErrorMessage = "The field Brand must be a text with a minimum length of {2} and a maximum length of {1}.")]
        public string Brand { get; set; }

        [Required]
        [StringLength(
            CarModelMaxLength,
            MinimumLength = CarModelMinLength,
            ErrorMessage = "The field Model must be a text with a minimum length of {2} and a maximum length of {1}.")]
        public string Model { get; set; }

        [Required]
        [Url]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; }

        [Required]
        [StringLength(
            CarDescriptionMaxLength,
            MinimumLength = CarDescriptionMinLength,
            ErrorMessage = "The field Description must be a text with a minimum length of {2} and a maximum length of {1}.")]
        public string Description { get; set; }

        [Range(CarYearMinValue, CarYearMaxValue)]
        public int Year { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<CarCategoryViewModel> Categories { get; set; }
    }
}
