using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NextLevelBJJ.Application;
using NextLevelBJJ.Application.PassTypes.DTO;
using NextLevelBJJ.Application.PassTypes.Queries;
using System.Threading.Tasks;

namespace NextLevelBJJ.Infrastructure.EF.Queries.PassTypes
{
    public class GetPassTypeHandler : IQueryHandler<GetPassType, PassTypeDto>
    {
        private readonly NextLevelBJJContext _context;
        private readonly IMapper _mapper;

        public GetPassTypeHandler(NextLevelBJJContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PassTypeDto> HandleAsync(GetPassType query)
        {
            var passType = await _context.PassTypes.SingleOrDefaultAsync(pt => pt.Id == query.Id);

            return _mapper.Map<PassTypeDto>(passType);
        }
    }
}
