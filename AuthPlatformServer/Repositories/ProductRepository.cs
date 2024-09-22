using AuthPlatformServer.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthPlatformServer.Repositories
{
    public class ProductRepository
    {
        private readonly AuthPlatformDbContext _dbContext;

        public ProductRepository(AuthPlatformDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ProductEntity>> Get()
        {
            return await _dbContext.Products.AsNoTracking().ToListAsync();
        }

        public async Task Create(string title, int period, string description)
        {
            var product = new ProductEntity
            {
                Id = Guid.NewGuid(),
                Title = title,
                Period = period,
                Description = description
            };
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var product = await _dbContext.Products.FindAsync(id);
            if (product != null)
            {
                var keys = await _dbContext.Keys
                    .Where(k => k.ProductId == id)
                    .ToListAsync();

                _dbContext.Keys.RemoveRange(keys);

                _dbContext.Products.Remove(product);

                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task Change(Guid id, string title, int period, string description)
        {
            var product = await _dbContext.Products.FindAsync(id);
            if (product != null)
            {
                product.Title = title;
                product.Period = period;
                product.Description = description;
                _dbContext.Products.Update(product);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
