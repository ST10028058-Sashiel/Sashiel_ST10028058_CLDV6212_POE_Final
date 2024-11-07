using System.ComponentModel.DataAnnotations;

namespace ST10028058_CLDV6212_POE_Final.Models
{
    public class Product
    {

        [Key]
        public int Product_Id { get; set; }
        public string? Product_Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public double Product_Price { get; set; }
        public int Quantity { get; set; }
    }
}
