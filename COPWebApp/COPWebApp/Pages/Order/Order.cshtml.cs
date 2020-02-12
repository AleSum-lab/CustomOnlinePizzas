using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace COPWebApp
{
    public class OrderModel : PageModel
    {
        [BindProperty]
        public Order Order { get; set; }

        public bool TomatoChecked { get; set; }
        public bool MozzarellaChecked { get; set; }
        public bool HamChecked { get; set; }
        public bool KebabChecked { get; set; }

        public int Total { get; set; }

        public async Task<IActionResult> OnGet()
        {
            return Page();
        }
    }
}