using Dapper;
using Microsoft.Extensions.Options;
using RedVentures.TravelApi.Core;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace RedVentures.TravelApi.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppSettings _settings;

        public UserRepository(IOptions<AppSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task<int?>GetIdByUid(Guid userUid)
        {
            const string sql = "SELECT u.UserId FROM [User] u WHERE u.UserUid = @userUid";

            using (var conn = new SqlConnection(_settings.ConnectionStrings.TravelApiDatabase))
            {
                var userId = await conn.QuerySingleOrDefaultAsync<int?>(sql, new { userUid = userUid });

                return userId;
            }
        }
    }
}
