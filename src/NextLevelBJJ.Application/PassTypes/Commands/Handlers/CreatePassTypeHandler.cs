using AutoMapper;
using NextLevelBJJ.Application.PassTypes.DTO;
using NextLevelBJJ.Core.Entities;
using NextLevelBJJ.Core.Repositories;
using System.Threading.Tasks;

namespace NextLevelBJJ.Application.PassTypes.Commands.Handlers
{
    public class CreatePassTypeHandler : ICommandHandler<CreatePassType>
    {
        private readonly IPassTypeRepository _passTypeRepository;
        private readonly IMapper _mapper;

        public CreatePassTypeHandler(IPassTypeRepository passTypeRepository, IMapper mapper)
        {
            _passTypeRepository = passTypeRepository;
            _mapper = mapper;
        }

        public async Task HandleAsync(CreatePassType command)
        {
            var passTypeDto = _mapper.Map<PassTypeDto>(command);
            var passTypeEntity = _mapper.Map<PassType>(passTypeDto);

            await _passTypeRepository.AddAsync(passTypeEntity);
        }
    }
}
