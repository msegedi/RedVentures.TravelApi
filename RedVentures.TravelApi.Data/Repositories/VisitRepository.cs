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
    }
}
