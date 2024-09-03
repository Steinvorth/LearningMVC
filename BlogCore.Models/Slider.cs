using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.Models
{
    public class Slider
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "A slider name is required!")]
        [Display(Name = "Slider Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "A slider state is required!")]
        public bool State { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Image")]
        public string ImageURL { get; set; }
    }
}
