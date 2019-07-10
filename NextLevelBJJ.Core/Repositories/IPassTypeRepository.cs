using NextLevelBJJ.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NextLevelBJJ.Core.Repositories
{
    public interface IPassTypeRepository
    {
        Task<PassType> GetPassTypeAsync(Guid id);
        Task AddPassTypeAsync(PassType passType);
        Task DeletePassTypeAsync(Guid id);
    }
}
