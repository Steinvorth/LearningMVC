using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        //Name
        [Required(ErrorMessage = "A category name is required!")] // This is the message that will be displayed if the user tries to submit the form without entering a category name
        [Display(Name = "Category Name")] // This is the name that will be displayed in the form
        public string Name { get; set; }

        //order
        [Display(Name = "Order View")]
        [Range(1,100, ErrorMessage="The value must be between 1 and 100!")]
        public int? Order { get; set; }



    }
}
