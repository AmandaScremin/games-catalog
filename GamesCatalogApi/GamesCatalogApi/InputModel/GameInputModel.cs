using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GamesCatalogApi.InputModel
{
    public class GameInputModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "The games's name must contain betweent 3 and 100 characters")]

        public string Name { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "The games's producer must contain betweent 3 and 100 characters")]

        public string Producer { get; set; }
        [Required]
        [Range(1, 1000, ErrorMessage = "The price must be between 1 and 1000 Reais")]

        public double Price { get; set; }
    }
}
