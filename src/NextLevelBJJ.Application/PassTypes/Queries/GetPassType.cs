using NextLevelBJJ.Application.PassTypes.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace NextLevelBJJ.Application.PassTypes.Queries
{
    public class GetPassType : IQuery<PassTypeDto>
    {
        public Guid Id { get; set; }
    }
}
