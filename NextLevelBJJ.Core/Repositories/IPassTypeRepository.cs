using NextLevelBJJ.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NextLevelBJJ.Core.Repositories
{
    public interface IPassTypeRepository
    {
        Task<IEnumerable<PassType>> GetAsync();
        Task<PassType> GetAsync(Guid id);
        Task AddAsync(PassType passType);
        Task DeleteAsync(Guid id);
    }
}
