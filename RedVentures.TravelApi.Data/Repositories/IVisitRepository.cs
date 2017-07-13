using RedVentures.TravelApi.Data.TransferObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedVentures.TravelApi.Data.Repositories
{
    public interface IVisitRepository
    {
        Task<IList<CityWithState>> GetDistinctCitiesVisitedByUser(int userId);
        Task<IList<string>> GetDistinctStatesVisitedByUser(int userId);
        Task<int?> GetIdByUidAsync(Guid visitUid);
        Task<Guid> AddAsync(int userId, int cityId);
        Task DeleteAsync(int visitId);
    }
}
