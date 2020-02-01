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
    public class ProductOptionRepository : IProductOptionRepository
    {
        private readonly ProductsServiceDbContext _dbContext;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public ProductOptionRepository(ProductsServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public async Task<List<ProductOption>> GetByProductAsync(Guid productId)
        {
            var dbEntities = await _dbContext.ProductOptions
                .Where(x => x.ProductId == productId)
                .OrderBy(x => x.Name)
                .ThenBy(x => x.Description)
                .ToListAsync();

            var productOptions = new List<ProductOption>();
            productOptions.AddRange(dbEntities.Select(dbEntity => Mapper.Map<ProductOption>(dbEntity)));

            return productOptions;
        }

        /// <inheritdoc />
        public async Task<ProductOption> GetByIdAsync(Guid id)
        {
            var dbEntity = await _dbContext.ProductOptions.FindAsync(id);
            return dbEntity == null ? null : Mapper.Map<ProductOption>(dbEntity);
        }

        /// <inheritdoc />
        public async Task<ProductOption> CreateAsync(ProductOption ProductOption)
        {
            var dbEntity = _dbContext.ProductOptions.Add(Mapper.Map<Entities.ProductOption>(ProductOption));
            await _dbContext.SaveChangesAsync();
            return Mapper.Map<ProductOption>(dbEntity.Entity);
        }

        /// <inheritdoc />
        public async Task<bool> UpdateAsync(ProductOption productOption)
        {
            var dbEntity = _dbContext.ProductOptions.Find(productOption.Id);

            dbEntity.ProductId = productOption.ProductId;
            dbEntity.Description = productOption.Description;
            dbEntity.Name = productOption.Name;

            _dbContext.ProductOptions.Update(dbEntity);
            var count = await _dbContext.SaveChangesAsync();
            return count > 0;
        }

        /// <inheritdoc />
        public async Task<bool> DeleteAsync(Guid id)
        {
            var dbEntity = await _dbContext.ProductOptions.FindAsync(id);

            _dbContext.ProductOptions.Remove(dbEntity);
            var count = await _dbContext.SaveChangesAsync();
            return count > 0;
        }
    }
}
