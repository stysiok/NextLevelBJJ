using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NextLevelBJJ.Application.Students.Commands;
using NextLevelBJJ.Application.Students.DTO;
using NextLevelBJJ.Core.Repositories;

namespace NextLevelBJJ.Application.Students.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IJwtProvider _jwtProvider;

        public IdentityService(IStudentRepository studentRepository, IJwtProvider jwtProvider)
        {
            _studentRepository = studentRepository;
            _jwtProvider = jwtProvider;
        }

        public async Task<JwtDto> SignInAsync(SignIn command)
        {
            var student = await _studentRepository.GetAsync(command.Id);

            if(student is null)
            {
                throw new Exception("Invalid data");
            }

            return _jwtProvider.CreateToken(student.Id.ToString("N"), student.Role);
        }
    }
}
