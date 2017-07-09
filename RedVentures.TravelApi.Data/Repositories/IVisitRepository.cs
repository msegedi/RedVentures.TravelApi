using System;
using System.Threading.Tasks;

namespace RedVentures.TravelApi.Data.Repositories
{
    public interface IVisitRepository
    {
        Task<Guid> AddAsync(int userId, int cityId);
    }
}
