using System.ComponentModel.DataAnnotations;

namespace ST10028058_CLDV6212_POE_Final.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "Please select a product.")]
        public int ProductId { get; set; }
        public Product? Product { get; set; }  // Make nullable

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        //[Required(ErrorMessage = "Please select a customer.")]
        //public int CustomerProfileId { get; set; }
        //public CustomerProfile? CustomerProfile { get; set; }  // Make nullable
    }

}
