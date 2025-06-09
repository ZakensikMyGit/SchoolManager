using SchoolManager.Domain.Model;
using System.Collections.Generic;

namespace SchoolManager.Domain.Interfaces
{
    public interface IPositionRepository
    {
        List<Position> GetAllPositions();
        Position GetPositionById(int id);
    }
}
