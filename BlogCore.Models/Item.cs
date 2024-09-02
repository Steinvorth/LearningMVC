using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "A item name is required!")]
        [Display(Name = "Item Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "A item description is required!")]
        public string Description { get; set; }

        [Display(Name = "Creation Date")]
        public string CreationDate { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Image")]
        public string ImageURL { get; set; }

        [Display(Name = "Category")]
        [Required(ErrorMessage = "A category is required!")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
    }
}
