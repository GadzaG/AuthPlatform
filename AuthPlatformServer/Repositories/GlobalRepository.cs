using AuthPlatformServer.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthPlatformServer.Repositories
{
    public class GlobalRepository
    {
        private readonly AuthPlatformDbContext _dbContext;

        public GlobalRepository(AuthPlatformDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<GlobalEntity>> Get()
        {
            return await _dbContext.Globals.AsNoTracking().ToListAsync();
        }

        public async Task Change(Guid id, string key, string value)
        {
            var global = await _dbContext.Globals.FindAsync(id);
            if (global != null)
            {
                global.KeyText = key;
                global.Value = value;
                _dbContext.Globals.Update(global);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task Delete(Guid id)
        {
            var global = await _dbContext.Globals.FindAsync(id);
            if (global != null)
            {
                _dbContext.Globals.Remove(global);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task Add(string key, string value)
        {
            var global = new GlobalEntity
            {
                Id = Guid.NewGuid(),
                KeyText = key,
                Value = value
            };
            await _dbContext.Globals.AddAsync(global);
            await _dbContext.SaveChangesAsync();
        }

        //api function
        public async Task<string?> GetValueByKey(string key)
        {
            var global = await _dbContext.Globals.FirstOrDefaultAsync(g => g.KeyText == key);
            return global?.Value;
        }
    }
}
