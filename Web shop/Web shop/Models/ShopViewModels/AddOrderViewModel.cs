using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Webshop.Models.ShopViewModels
{
    public class AddOrderViewModel
    {
        [Required]
        [MaxLength(255)]
        [Display(Name = "Name")]
        public string BuyerName { get; set; }
        [Required]
        [MaxLength(255)]
        [Display(Name = "Surname")]
        public string BuyerSurname { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Contact Email")]
        public string ContactEmail { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid Phone Number")]
        [RegularExpression(@"^([0-9]{8,12})$", ErrorMessage = "Invalid Phone Number.")]
        [Display(Name = "Contact phone number")]
        public string PhoneNumber { get; set; }
        [Required]
        [MaxLength(255)]
        [Display(Name = "Adress")]
        public string Adress { get; set; }
        
    }
}
