using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Products.Service.Domain;
using Products.Service.Interfaces.Repository;
using System.Linq;

namespace Products.Service.Repositories.Repositories
{
    /// <inheritdoc />
    public class ProductRepository : IProductRepository
    {
        private readonly ProductsServiceDbContext _dbContext;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public ProductRepository(ProductsServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public async Task<List<Product>> GetAsync(string name, bool exactMatch = false)
        {
            var dbEntities = await (string.IsNullOrWhiteSpace(name) ? _dbContext.Products
                : (exactMatch ? _dbContext.Products.Where(p => p.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                : _dbContext.Products.Where(p => p.Name.Contains(name, StringComparison.InvariantCultureIgnoreCase))))
                .OrderBy(p => p.Name)
                .ThenBy(p => p.Description)
                .ToListAsync();

            var products = new List<Product>();
            products.AddRange(dbEntities.Select(dbEntity => Mapper.Map<Product>(dbEntity)));

            return products;
        }

        /// <inheritdoc />
        public async Task<Product> GetByIdAsync(Guid Id)
        {
            var dbEntity = await _dbContext.Products.FindAsync(Id);
            return dbEntity == null ? null : Mapper.Map<Product>(dbEntity);
        }

        /// <inheritdoc />
        public async Task<Product> CreateAsync(Product product)
        {
            var dbEntity =_dbContext.Products.Add(Mapper.Map<Entities.Product>(product));
            await _dbContext.SaveChangesAsync();
            return Mapper.Map<Product>(dbEntity.Entity);
        }

        /// <inheritdoc />
        public async Task<bool> DeleteAsync(Guid id)
        {
            var dbEntity = await _dbContext.Products
                .Include(p => p.ProductOptions)
                .FirstAsync(p => p.Id == id);

            _dbContext.Products.Remove(dbEntity);
            var count = await _dbContext.SaveChangesAsync();
            return count > 0;
        }

        /// <inheritdoc />
        public async Task<bool> UpdateAsync(Product product)
        {
            var dbEntity = _dbContext.Products.Find(product.Id);

            dbEntity.DeliveryPrice = product.DeliveryPrice;
            dbEntity.Description = product.Description;
            dbEntity.Name = product.Name;
            dbEntity.Price = product.Price;

            _dbContext.Products.Update(dbEntity);
            var count = await _dbContext.SaveChangesAsync();
            return count > 0;
        }
    }
}
