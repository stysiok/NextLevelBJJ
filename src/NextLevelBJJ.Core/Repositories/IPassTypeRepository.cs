using NextLevelBJJ.Core.Entities;
using System;
using System.Threading.Tasks;

namespace NextLevelBJJ.Core.Repositories
{
    public interface IPassTypeRepository
    {
        Task<PassType> GetAsync(Guid id);
        Task AddAsync(PassType passType);
        Task DeleteAsync(Guid id);
    }
}
