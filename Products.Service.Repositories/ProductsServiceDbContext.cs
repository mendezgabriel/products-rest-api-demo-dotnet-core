using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Products.Service.Repositories.Entities;

namespace Products.Service.Repositories
{
    public class ProductsServiceDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductOption> ProductOptions { get; set; }

        /// <inheritdoc />
        public ProductsServiceDbContext(DbContextOptions<ProductsServiceDbContext> options)
            : base(options)
        { }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
