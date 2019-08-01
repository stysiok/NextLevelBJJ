using AutoMapper;
using NextLevelBJJ.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NextLevelBJJ.Application.PassTypes.Commands.Handlers
{
    public class DeletePassTypeHandler : ICommandHandler<DeletePassType>
    {
        private readonly IPassTypeRepository _passTypeRepository;

        public DeletePassTypeHandler(IPassTypeRepository passTypeRepository)
        {
            _passTypeRepository = passTypeRepository;
        }

        public async Task HandleAsync(DeletePassType command)
        {
            await _passTypeRepository.DeleteAsync(command.Id);
        }
    }
}
