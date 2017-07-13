using Microsoft.AspNetCore.Mvc;
using RedVentures.TravelApi.Data.Repositories;
using RedVentures.TravelApi.Web.Mappers;
using RedVentures.TravelApi.Web.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RedVentures.TravelApi.Web.Controllers
{
    [Route("user")]
    public class UserController : Controller
    {
        private readonly IStateRepository _stateRepo;
        private readonly ICityRepository _cityRepo;
        private readonly IUserRepository _userRepo;
        private readonly IVisitRepository _visitRepo;
        private readonly IVisitedCityMapper _visitedCityMapper;

        public UserController(
            IStateRepository stateRepo,
            ICityRepository cityRepo,
            IUserRepository userRepo,
            IVisitRepository visitRepo,
            IVisitedCityMapper visitedCityMapper)
        {
            _stateRepo = stateRepo;
            _cityRepo = cityRepo;
            _userRepo = userRepo;
            _visitRepo = visitRepo;
            _visitedCityMapper = visitedCityMapper;
        }

        [HttpGet("{userUid}/visits")]
        public async Task<IActionResult> GetCityVisits(Guid userUid)
        {
            var userId = await _userRepo.GetIdByUid(userUid);

            if (!userId.HasValue)
                return NotFound();

            var daCities = await _visitRepo.GetDistinctCitiesVisitedByUser(userId.Value);

            var cities = _visitedCityMapper.GetListFromDataTransferObjects(daCities);

            return Ok(cities);
        }

        [HttpGet("{userUid}/visits/states")]
        public async Task<IActionResult> GetStateVisits(Guid userUid)
        {
            var userId = await _userRepo.GetIdByUid(userUid);

            if (!userId.HasValue)
                return NotFound();

            var daStates = await _visitRepo.GetDistinctStatesVisitedByUser(userId.Value);

            return Ok(daStates);
        }

        [HttpGet("{userUid}/visits/{visitUid}")]
        public async Task<IActionResult> GetVisit(Guid userUid, Guid visitUid)
        {
            throw new NotImplementedException();
        }

        [HttpPost("{userUid}/visits")]
        public async Task<IActionResult> PostVisit(Guid userUid, [FromBody] CreateVisit model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList());

            var userId = await _userRepo.GetIdByUid(userUid);

            if (!userId.HasValue)
                return NotFound();

            var stateId = await _stateRepo.GetIdByCodeAsync(model.State);

            if (!stateId.HasValue)
                return BadRequest($"State '{model.State}' not found.");

            var cityId = await _cityRepo.GetCityIdAsync(stateId.Value, model.City);

            if (!cityId.HasValue)
                return BadRequest($"City '{model.City}' not found for state '{model.State}'.");

            var visitUid = await _visitRepo.AddAsync(userId.Value, cityId.Value);

            return CreatedAtAction(nameof(GetVisit), new { userUid = userUid, visitUid = visitUid }, new { visitUid = visitUid });
        }

        [HttpDelete("{userUid}/visits/{visitUid}")]
        public async Task<IActionResult> DeleteVisit(Guid userUid, Guid visitUid)
        {
            /*
             * Since VisitUids are unique, and we don't yet have any authorization logic tied to UserUid, we can skip UserUid validation.
             * However, for completeness, it would probably be a good idea to make sure the UserUid exists and that the given VisitUid belongs to it.
             */

            var visitId = await _visitRepo.GetIdByUidAsync(visitUid);

            if (!visitId.HasValue)
                return NotFound();

            await _visitRepo.DeleteAsync(visitId.Value);

            return NoContent();
        }
    }
}
