using NextLevelBJJ.Application.PassTypes.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace NextLevelBJJ.Application.PassTypes.Queries
{
    public class GetPassTypes : IQuery<IEnumerable<PassTypeDto>>
    {
        public decimal? Price { get; set; }
        public int? Entries { get; set; }
    }
}
