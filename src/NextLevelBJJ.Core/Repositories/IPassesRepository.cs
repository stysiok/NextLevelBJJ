using System;
using System.Threading.Tasks;
using NextLevelBJJ.Core.Entities;

namespace NextLevelBJJ.Core.Repositories
{
    public interface IPassesRepository
    {
        Task<Pass> Get(Guid id);
    }
}
