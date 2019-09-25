using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NextLevelBJJ.Core.Entities;
using NextLevelBJJ.Core.Repositories;

namespace NextLevelBJJ.Infrastructure.EF.Repositories
{
    public class EFPassesRepository : IPassesRepository
    {
        private readonly NextLevelBJJContext _context;

        public EFPassesRepository(NextLevelBJJContext context)
        {
            _context = context;
        }

        public async Task<Pass> Get(Guid id) =>
            await _context.Passes.SingleOrDefaultAsync(p => p.Id == id);
        
    }
}