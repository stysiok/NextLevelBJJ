using NextLevelBJJ.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NextLevelBJJ.Application.PassTypes.Commands.Handlers
{
    public class CreatePassTypeHandler : ICommandHandler<CreatePassType>
    {
        private readonly IPassTypeRepository _passTypeRepository;

        public CreatePassTypeHandler(IPassTypeRepository passTypeRepository)
        {
            _passTypeRepository = passTypeRepository;
        }

        public async Task HandleAsync(CreatePassType command)
        {
            //await _passTypeRepository.AddPassTypeAsync(passType);
        }
    }
}
