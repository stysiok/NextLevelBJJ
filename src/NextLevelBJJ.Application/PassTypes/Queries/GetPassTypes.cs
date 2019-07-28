using NextLevelBJJ.Application.PassTypes.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace NextLevelBJJ.Application.PassTypes.Queries
{
    public class GetPassTypes : IQuery<IEnumerable<PassTypeDto>>
    {
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? MinEntries { get; set; }
        public int? MaxEntries { get; set; }
        public string Name { get; set; }
        public bool IsOpen { get; set; }

    }
}
