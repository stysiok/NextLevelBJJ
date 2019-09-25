using System;
using NextLevelBJJ.Application.Passes.DTO;

namespace NextLevelBJJ.Application.Passes.Queries
{
    public class GetPass : IQuery<PassDto>
    {
        public Guid Id { get; set; }
    }
}
