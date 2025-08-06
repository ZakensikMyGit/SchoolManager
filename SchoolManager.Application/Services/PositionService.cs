using AutoMapper;
using SchoolManager.Application.Interfaces;
using SchoolManager.Application.ViewModels.Position;
using SchoolManager.Domain.Interfaces;
using SchoolManager.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolManager.Application.Services
{
    public class PositionService : IPositionService
    {
        private readonly IPositionRepository _positionRepository;
        private readonly IMapper _mapper;

        public PositionService(IPositionRepository positionRepository, IMapper mapper)
        {
            _positionRepository = positionRepository;
            _mapper = mapper;
        }

        public Task<List<Position>> GetAllAsync()
        {
            return _positionRepository.GetAllPositionsAsync();
        }

        public async Task<int> AddPositionAsync(NewPositionVm model)
        {
            var entity = _mapper.Map<Position>(model);
            return await _positionRepository.AddPositionAsync(entity);
        }
    }
}