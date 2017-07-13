using Dapper;
using Microsoft.Extensions.Options;
using RedVentures.TravelApi.Core;
using RedVentures.TravelApi.Data.TransferObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace RedVentures.TravelApi.Data.Repositories
{
    public class VisitRepository : IVisitRepository
    {
        private readonly AppSettings _settings;

        public VisitRepository(IOptions<AppSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task<IList<CityWithState>> GetDistinctCitiesVisitedByUser(int userId)
        {
            var sql = $@"
SELECT DISTINCT c.Name AS {nameof(CityWithState.CityName)},
s.Code AS {nameof(CityWithState.StateCode)}
FROM Visit v
INNER JOIN City c ON c.CityId = v.CityId
INNER JOIN State s ON s.StateId = c.StateId
WHERE v.UserId = @userId
ORDER BY s.Code, c.Name
";

            using (var conn = new SqlConnection(_settings.ConnectionStrings.TravelApiDatabase))
            {
                var cities = await conn.QueryAsync<CityWithState>(sql, new { userId = userId });

                return cities.ToList();
            }
        }

        public async Task<IList<string>> GetDistinctStatesVisitedByUser(int userId)
        {
            var sql = $@"
SELECT DISTINCT s.Code AS {nameof(CityWithState.StateCode)}
FROM Visit v
INNER JOIN City c ON c.CityId = v.CityId
INNER JOIN State s ON s.StateId = c.StateId
WHERE v.UserId = @userId
ORDER BY s.Code
";

            using (var conn = new SqlConnection(_settings.ConnectionStrings.TravelApiDatabase))
            {
                var states = await conn.QueryAsync<string>(sql, new { userId = userId });

                return states.ToList();
            }
        }

        public async Task<int?> GetIdByUidAsync(Guid visitUid)
        {
            const string sql = "SELECT VisitId FROM Visit WHERE VisitUid = @visitUid";

            using (var conn = new SqlConnection(_settings.ConnectionStrings.TravelApiDatabase))
            {
                var visitId = await conn.QuerySingleOrDefaultAsync<int?>(sql, new { visitUid = visitUid });

                return visitId;
            }
        }

        public async Task<Guid> AddAsync(int userId, int cityId)
        {
            var visitUid = Guid.NewGuid();

            const string sql = "INSERT INTO Visit (VisitUid, UserId, CityId) VALUES (@visitUid, @userId, @cityId)";

            using (var conn = new SqlConnection(_settings.ConnectionStrings.TravelApiDatabase))
            {
                await conn.ExecuteAsync(sql, new { visitUid = visitUid, userId = userId, cityId = cityId });

                return visitUid;
            }
        }

        public async Task DeleteAsync(int visitId)
        {
            const string sql = "DELETE FROM Visit WHERE VisitId = @visitId";

            using (var conn = new SqlConnection(_settings.ConnectionStrings.TravelApiDatabase))
            {
                await conn.ExecuteAsync(sql, new { visitId = visitId });
            }
        }

    }
}
