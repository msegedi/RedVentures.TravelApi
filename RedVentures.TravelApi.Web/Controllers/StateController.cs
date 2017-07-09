using Microsoft.AspNetCore.Mvc;
using RedVentures.TravelApi.Data.Repositories;
using System.Threading.Tasks;

namespace RedVentures.TravelApi.Web.Controllers
{
    [Route("state")]
    public class StateController : Controller
    {
        private readonly IStateRepository _stateRepo;
        private readonly ICityRepository _cityRepo;

        public StateController(
            IStateRepository stateRepo,
            ICityRepository cityRepo)
        {
            _stateRepo = stateRepo;
            _cityRepo = cityRepo;
        }

        [HttpGet("{stateCode}/cities")]
        public async Task<IActionResult> GetCities(string stateCode)
        {
            var stateId = await _stateRepo.GetIdByCode(stateCode);

            if (!stateId.HasValue)
                return NotFound();

            var cities = await _cityRepo.GetCityNamesByState(stateId.Value);

            return Ok(cities);
        }
    }
}
