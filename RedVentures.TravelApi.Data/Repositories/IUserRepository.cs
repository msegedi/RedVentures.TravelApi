using System;
using System.Threading.Tasks;

namespace RedVentures.TravelApi.Data.Repositories
{
    public interface IUserRepository
    {
        Task<int?>GetIdByUid(Guid userUid);
    }
}
