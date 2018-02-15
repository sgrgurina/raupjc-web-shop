using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Webshop.Models.ShopViewModels
{
    public class AddCategoryViewModel
    {
        [Required]
        [MaxLength(255)]
        [Display(Name = "Category name")]
        public string Name { get; set; }
    }
}
