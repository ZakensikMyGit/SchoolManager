using SchoolManager.Domain.Interfaces;
using SchoolManager.Domain.Model;
using System.Collections.Generic;
using System.Linq;

namespace SchoolManager.Infrastructure.Repositories
{
    public class PositionRepository : IPositionRepository
    {
        private readonly Context _context;

        public PositionRepository(Context context)
        {
            _context = context;
        }

        public List<Position> GetAllPositions()
        {
            return _context.Positions.ToList();
        }

        public Position GetPositionById(int id)
        {
            return _context.Positions.FirstOrDefault(p => p.Id == id);
        }
    }
}
