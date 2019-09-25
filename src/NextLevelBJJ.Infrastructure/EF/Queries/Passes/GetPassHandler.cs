using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NextLevelBJJ.Application;
using NextLevelBJJ.Application.Passes.DTO;
using NextLevelBJJ.Application.Passes.Queries;

namespace NextLevelBJJ.Infrastructure.EF.Queries.Passes
{
    public class GetPassHandler : IQueryHandler<GetPass, PassDto>
    {
        private readonly IMapper _mapper;
        private readonly NextLevelBJJContext _context;

        public GetPassHandler(IMapper mapper, NextLevelBJJContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<PassDto> HandleAsync(GetPass query)
        {
            var result = await _context.Passes.SingleOrDefaultAsync(p => p.Id == query.Id);

            return _mapper.Map<PassDto>(result);
        }
    }
}
