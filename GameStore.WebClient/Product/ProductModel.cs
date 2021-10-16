using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameStore.WebClient.Product
{
    public class ProductModel
        : IValidatableObject
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        
        
        public decimal Price { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }

        public bool Published { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Price <= 0)
                yield return new ValidationResult("Price should have positive value.", new[] {nameof(Price)});
        }
    }
}