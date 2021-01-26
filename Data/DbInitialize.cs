using Pastel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pastel.Data
{
    public static class DbInitializer
    {
        public static void Initialize(PastelDataContext context)
        {
            context.Database.EnsureCreated();

            if (context.Products.Any())
            {
                return;
            }

            var products = new Product[]
            {
                new Product{Name="Bolo-Rei",Description="Não pode faltar na mesa de Natal. Uma tradição e uma perdição.",Price=21,ImageName="rei.jpg"},
                new Product{Name="Pão de Ló",Description="Especialidade de Felgueiras, o famoso pão-de-ló de Margaride",Price=17,ImageName="pao.jpg"},
                new Product{Name="Castanhas de ovos",Description="Doce conventual",Price=24,ImageName="castanhas.jpg"},
                new Product{Name="Ovos Moles",Description="Produto tradicional de Aveiro",Price=10,ImageName="ovos.jpg"},
                new Product{Name="Tarte de limão",Description="Tarte de limão merengada.",Price=12,ImageName="tarte.jpg"},
                new Product{Name="Bolo de Chocolate",Description="Bolo rico de chocolate, especial para quem não consegue resistir a um excesso calórico.",Price=14,ImageName="chocolate.jpg"},

            };

            foreach (Product product in products)
            {
                context.Products.Add(product);
            }
            context.SaveChanges();
        }
    }
}
