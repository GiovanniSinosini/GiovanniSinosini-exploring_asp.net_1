using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pastel.Data;
using Pastel.Model;
using Microsoft.EntityFrameworkCore;

namespace Pastel.Pages
{
    public class IndexModel : PageModel
    {
        private readonly PastelDataContext _context;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(PastelDataContext context)
        {
            _context = context;
        }
        public IList<Product> Products { get; set; }
        public Product RandomProduct { get; set; }
        
        public async Task OnGetAsync()
        {
            Products = await _context.Products.AsNoTracking().ToListAsync();
            RandomProduct = Products[new Random().Next(Products.Count)];
        }
    }
}
