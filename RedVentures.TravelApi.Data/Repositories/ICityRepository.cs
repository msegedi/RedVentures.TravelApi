using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedVentures.TravelApi.Data.Repositories
{
    public interface ICityRepository
    {
        Task<IList<string>> GetCityNamesByStateAsync(int stateId);
        Task<int?> GetCityIdAsync(int stateId, string cityName);
    }
}
