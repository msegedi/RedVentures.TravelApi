using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedVentures.TravelApi.Data.Repositories
{
    public interface ICityRepository
    {
        Task<IList<string>> GetCityNamesByState(int stateId);
    }
}
