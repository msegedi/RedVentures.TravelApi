using RedVentures.TravelApi.Data.TransferObjects;
using RedVentures.TravelApi.Web.Models;
using System.Collections.Generic;

namespace RedVentures.TravelApi.Web.Mappers
{
    public interface IVisitedCityMapper
    {
        IList<VisitedCity> GetListFromDataTransferObjects(IList<CityWithState> cities);
    }
}
