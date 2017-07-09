using Dapper;
using Microsoft.Extensions.Options;
using RedVentures.TravelApi.Core;
using System;
using System.Data.SqlClient;
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

        public async Task<int?> GetIdByUidAsync(Guid visitUid)
        {
            const string sql = "SELECT VisitId FROM Visit WHERE VisitUid = @visitUid";

            using (var conn = new SqlConnection(_settings.ConnectionStrings.TravelApiDatabase))
            {
                var visitId = await conn.QuerySingleOrDefaultAsync<int?>(sql.ToString(), new { visitUid = visitUid });

                return visitId;
            }
        }

        public async Task<Guid> AddAsync(int userId, int cityId)
        {
            var visitUid = Guid.NewGuid();

            const string sql = "INSERT INTO Visit (VisitUid, UserId, CityId) VALUES (@visitUid, @userId, @cityId)";

            using (var conn = new SqlConnection(_settings.ConnectionStrings.TravelApiDatabase))
            {
                await conn.ExecuteAsync(sql.ToString(), new { visitUid = visitUid, userId = userId, cityId = cityId });

                return visitUid;
            }
        }

        public async Task DeleteAsync(int visitId)
        {
            const string sql = "DELETE FROM Visit WHERE VisitId = @visitId";

            using (var conn = new SqlConnection(_settings.ConnectionStrings.TravelApiDatabase))
            {
                await conn.ExecuteAsync(sql.ToString(), new { visitId = visitId });
            }
        }

    }
}
