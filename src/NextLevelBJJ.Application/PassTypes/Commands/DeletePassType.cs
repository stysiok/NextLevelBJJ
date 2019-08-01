using System;
using System.Collections.Generic;
using System.Text;

namespace NextLevelBJJ.Application.PassTypes.Commands
{
    public class DeletePassType : ICommand
    {
        public Guid Id { get; set; }
    }
}
