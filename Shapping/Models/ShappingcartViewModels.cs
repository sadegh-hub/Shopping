using System.ComponentModel.DataAnnotations;

namespace Shapping.Models
{
    public class ShappingcartViewModels
    {
        [Key]
        public int ID { get; set; }
        public Productkala product { get; set; }
        [Display(Name = "قیمت")]
        public int Quntity { get; set; }

    }
}