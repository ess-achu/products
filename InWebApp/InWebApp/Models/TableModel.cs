using System.ComponentModel.DataAnnotations;

namespace InWebApp.Models
{
    public class ProductModel
    {
        public long Id { get; set; }

        [Required]
        public string productname { get; set; }

        [Required]
        public string category { get; set; }

        [Required]
        public double rate { get; set; }
    }
}
