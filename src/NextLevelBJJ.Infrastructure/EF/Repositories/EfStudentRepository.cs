using Microsoft.EntityFrameworkCore;
using NextLevelBJJ.Core.Entities;
using NextLevelBJJ.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NextLevelBJJ.Infrastructure.EF.Repositories
{
    public class EfStudentRepository : IStudentRepository
    {
        private readonly NextLevelBJJContext _context;

        public EfStudentRepository(NextLevelBJJContext context)
        {
            _context = context;
        }

        public async Task<Student> GetAsync(Guid id) =>
            await _context.Students.SingleOrDefaultAsync(s => s.Id == id);
    }
}
