using System;
using System.Threading.Tasks;

namespace RedVentures.TravelApi.Data.Repositories
{
    public interface IVisitRepository
    {
        Task<int?> GetIdByUidAsync(Guid visitUid);
        Task<Guid> AddAsync(int userId, int cityId);
        Task DeleteAsync(int visitId);
    }
}
