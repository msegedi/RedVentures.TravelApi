using RedVentures.TravelApi.Data.TransferObjects;
using RedVentures.TravelApi.Web.Models;
using System.Collections.Generic;
using System.Linq;

namespace RedVentures.TravelApi.Web.Mappers
{
    public class VisitedCityMapper : IVisitedCityMapper
    {
        public IList<VisitedCity> GetListFromDataTransferObjects(IList<CityWithState> cities)
        {
            return cities.Select(c => new VisitedCity
            {
                CityName = c.CityName,
                StateCode = c.StateCode
            }).ToList();
        }
    }
}
