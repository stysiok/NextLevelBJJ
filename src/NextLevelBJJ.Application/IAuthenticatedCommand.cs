﻿using System;
using System.Collections.Generic;
using System.Text;

namespace NextLevelBJJ.Application
{
    public interface IAuthenticatedCommand : ICommand
    {
        Guid UserId { get; set; }
    }
}
