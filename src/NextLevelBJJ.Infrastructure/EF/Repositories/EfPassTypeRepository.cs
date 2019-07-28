using Microsoft.EntityFrameworkCore;
using NextLevelBJJ.Core.Entities;
using NextLevelBJJ.Core.Repositories;
using System;
using System.Threading.Tasks;

namespace NextLevelBJJ.Infrastructure.EF.Repositories
{
    public class EfPassTypeRepository : IPassTypeRepository
    {
        private readonly NextLevelBJJContext _context;

        public EfPassTypeRepository(NextLevelBJJContext context)
        {
            _context = context;
        }

        public async Task AddAsync(PassType passType)
        {
            await _context.PassTypes.AddAsync(passType);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var passType = await GetAsync(id);

            if (passType is null)
            {
                return;
            }

            _context.PassTypes.Remove(passType);
            await _context.SaveChangesAsync();
        }

        public async Task<PassType> GetAsync(Guid id)
            => await _context.PassTypes.SingleOrDefaultAsync(pt => pt.Id == id);
    }
}
