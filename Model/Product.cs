using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pastel.Model
{
    public class Product
    {
        public int ID { get; set; }
        [Display(Name="Nome")]
        public string Name { get; set; }
        [Display(Name = "Descrição")]
        public string Description { get; set; }
        [Display(Name = "Preço")]
        public decimal Price { get; set; }
        public string ImageName { get; set; }
    }
}
