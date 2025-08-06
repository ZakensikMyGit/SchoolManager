using SchoolManager.Application.ViewModels.Position;
using SchoolManager.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolManager.Application.Interfaces
{
    public interface IPositionService
    {
        Task<int> AddPositionAsync(NewPositionVm model);
        Task<List<Position>> GetAllAsync();
    }
}