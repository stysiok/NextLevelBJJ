using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NextLevelBJJ.Application;
using NextLevelBJJ.Application.PassTypes.DTO;
using NextLevelBJJ.Application.PassTypes.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevelBJJ.Infrastructure.EF.Queries
{
    public class GetPassTypesHandler : IQueryHandler<GetPassTypes, IEnumerable<PassTypeDto>>
    {
        private readonly NextLevelBJJContext _context;
        private readonly IMapper _mapper;

        public GetPassTypesHandler(NextLevelBJJContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PassTypeDto>> HandleAsync(GetPassTypes query)
        {
            var passTypes = _context.PassTypes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Name))
                passTypes = passTypes.Where(pt => pt.Name.Contains(query.Name));

            if (query.IsOpen)
                passTypes = passTypes.Where(pt => pt.IsOpen == query.IsOpen);

            if (query.MaxEntries.HasValue)
                passTypes = passTypes.Where(pt => pt.Entries <= query.MaxEntries);

            if (query.MinEntries.HasValue)
                passTypes = passTypes.Where(pt => pt.Entries >= query.MinEntries);

            if (query.MaxPrice.HasValue)
                passTypes = passTypes.Where(pt => pt.Price <= query.MaxPrice);

            if (query.MinPrice.HasValue)
                passTypes = passTypes.Where(pt => pt.Price >= query.MinPrice);


            return await passTypes.Select(pt => _mapper.Map<PassTypeDto>(pt)).ToListAsync();
        }
    }
}
