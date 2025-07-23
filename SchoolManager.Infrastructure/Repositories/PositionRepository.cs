using Microsoft.EntityFrameworkCore;
using SchoolManager.Domain.Interfaces;
using SchoolManager.Domain.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManager.Infrastructure.Repositories
{
    public class PositionRepository : IPositionRepository
    {
        private readonly Context _context;

        public PositionRepository(Context context)
        {
            _context = context;
        }

        public Task <List<Position>> GetAllPositionsAsync()
        {
            return _context.Positions.ToListAsync();
        }

        public Task <Position> GetPositionByIdAsync(int id)
        {
            return _context.Positions.FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
