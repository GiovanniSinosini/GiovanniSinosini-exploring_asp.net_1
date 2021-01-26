using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pastel.Model;


namespace Pastel.Data
{
    public class PastelDataContext : DbContext 
    {
        public PastelDataContext(DbContextOptions<PastelDataContext> options): base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("Product");
        }
    }
}
