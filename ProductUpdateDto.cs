using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace InventoryHubAPI.Dtos
{
    public class ProductUpdateDto
    {
        [Required]
        public string? Name { get; set; }

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        [Range(0.0, double.MaxValue)]
        public decimal Price { get; set; }

        public List<string> Validate()
        {
            var results = new List<string>();
            if (string.IsNullOrWhiteSpace(Name)) results.Add("Name is required.");
            if (Quantity < 0) results.Add("Quantity must be zero or positive.");
            if (Price < 0) results.Add("Price must be zero or positive.");
            return results;
        }
    }
}
