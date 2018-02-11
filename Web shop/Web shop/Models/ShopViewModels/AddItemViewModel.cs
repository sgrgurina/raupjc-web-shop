using System.ComponentModel.DataAnnotations;

namespace Webshop.Models.ShopViewModels
{
    public class AddItemViewModel
    {
        [Required]
        [Display(Name = "Item Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Item Price")]
        public decimal Price { get; set; }

        [Required]
        [MaxLength(255)]
        [Display(Name = "Item Description")]
        public string Description { get; set; }

        [Required]
        [MaxLength(255)]
        [Display(Name = "Item's categories")]
        public string Categories { get; set; }
    }
}
