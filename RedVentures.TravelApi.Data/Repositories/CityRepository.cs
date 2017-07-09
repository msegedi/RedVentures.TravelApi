using Dapper;
using Microsoft.Extensions.Options;
using RedVentures.TravelApi.Core;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace RedVentures.TravelApi.Data.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly AppSettings _settings;

        public CityRepository(IOptions<AppSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task<IList<string>> GetCityNamesByStateAsync(int stateId)
        {
            const string sql = @"
SELECT c.Name
FROM City c
WHERE c.StateId = @stateId
";
            using (var conn = new SqlConnection(_settings.ConnectionStrings.TravelApiDatabase))
            {
                var cities = await conn.QueryAsync<string>(sql.ToString(), new { stateId = stateId });

                return cities.ToList();
            }
        }

        public async Task<int?> GetCityIdAsync(int stateId, string cityName)
        {
            const string sql = @"
SELECT c.CityId
FROM City c
WHERE c.StateId = @stateId
AND c.Name = @name
";
            using (var conn = new SqlConnection(_settings.ConnectionStrings.TravelApiDatabase))
            {
                var cityId = await conn.QuerySingleOrDefaultAsync<int?>(sql.ToString(), new { stateId = stateId, name = cityName });

                return cityId;
            }
        }
    }
}
