using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NextLevelBJJ.Application.Students.Commands;
using NextLevelBJJ.Application.Students.DTO;

namespace NextLevelBJJ.Application.Students.Services
{
    public interface IIdentityService
    {
        Task<JwtDto> SignInAsync(SignIn command);
    }
}
