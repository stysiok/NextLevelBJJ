using System;
using System.Collections.Generic;
using System.Text;

namespace NextLevelBJJ.Application.PassTypes.DTO
{
    public class PassTypeDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Entries { get; set; }
        public bool IsOpen { get; set; }

        public PassTypeDto()
        {

        }
    }
}
