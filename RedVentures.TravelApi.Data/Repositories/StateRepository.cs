using Dapper;
using Microsoft.Extensions.Options;
using RedVentures.TravelApi.Core;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace RedVentures.TravelApi.Data.Repositories
{
    public class StateRepository : IStateRepository
    {
        private readonly AppSettings _settings;

        public StateRepository(IOptions<AppSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task<int?> GetIdByCode(string stateCode)
        {
            const string sql = @"
SELECT s.StateId
FROM State s
WHERE s.Code = @stateCode
";
            using (var conn = new SqlConnection(_settings.ConnectionStrings.TravelApiDatabase))
            {
                var stateId = await conn.QuerySingleOrDefaultAsync<int?>(sql.ToString(), new { stateCode = stateCode });

                return stateId;
            }
        }
    }
}
