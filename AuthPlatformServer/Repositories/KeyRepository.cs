using AuthPlatformServer.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace AuthPlatformServer.Repositories
{
    public class KeyRepository
    {
        private readonly AuthPlatformDbContext _dbContext;

        public KeyRepository(AuthPlatformDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<List<KeyEntity>> GetByProduct(Guid productId)
        {
            return await _dbContext.Keys
                .Where(k => k.ProductId == productId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task Create(Guid productId, int count)
        {
            var keys = new List<KeyEntity>();
            for (int i = 0; i < count; i++)
            {
                keys.Add(new KeyEntity
                {
                    Id = Guid.NewGuid(),
                    ProductId = productId,
                    KeyText = Guid.NewGuid().ToString(),
                    Status = Status.FREE
                });
            }
            await _dbContext.Keys.AddRangeAsync(keys);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(List<Guid> ids)
        {
            var keys = await _dbContext.Keys
                .Where(k => ids.Contains(k.Id))
                .ToListAsync();
            _dbContext.Keys.RemoveRange(keys);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Change(Guid id, Status status)
        {
            var key = await _dbContext.Keys.FindAsync(id);
            if (key != null)
            {
                key.Status = status;
                _dbContext.Keys.Update(key);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<JObject> GetData(Guid id)
        {
            var key = await _dbContext.Keys.FindAsync(id);
            return key?.Data;
        }

        //api
        public async Task AddData(string keyText, JObject data)
        {
            var key = await _dbContext.Keys.FirstOrDefaultAsync(k => k.KeyText == keyText);
            if (key != null)
            {
                key.Data = data;
                _dbContext.Keys.Update(key);
                await _dbContext.SaveChangesAsync();
            }
        }

        //api
        //public async Task KeyCheck(string keyText) { }
    }
}
