using SchoolManager.Domain.Model;
using System.Collections.Generic;

namespace SchoolManager.Domain.Interfaces
{
    public interface IPositionRepository
    {
        Task<List<Position>> GetAllPositionsAsync();
        Task<Position> GetPositionByIdAsync(int id);
        Task<int> AddPositionAsync(Position position);
    }
}
