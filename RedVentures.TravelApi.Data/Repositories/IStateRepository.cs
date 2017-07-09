using System.Threading.Tasks;

namespace RedVentures.TravelApi.Data.Repositories
{
    public interface IStateRepository
    {
        Task<int?> GetIdByCode(string stateCode);
    }
}
