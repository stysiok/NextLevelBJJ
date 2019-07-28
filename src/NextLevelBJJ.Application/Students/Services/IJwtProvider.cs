using System;
using System.Collections.Generic;
using System.Text;
using NextLevelBJJ.Application.Students.DTO;

namespace NextLevelBJJ.Application.Students.Services
{
    public interface IJwtProvider
    {
        JwtDto CreateToken(string studentId, string role);
    }
}
