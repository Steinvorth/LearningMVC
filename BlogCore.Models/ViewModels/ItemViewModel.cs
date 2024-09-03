using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;  // Use this namespace

namespace BlogCore.Models.ViewModels
{
    public class ItemViewModel
    {
        public Item Item { get; set; }

        public IEnumerable<SelectListItem> CategoryList { get; set; }
    }
}
