using AutoMapper;
using LegitFoodReviewApp.Dto;
using LegitFoodReviewApp.Interfaces;
using LegitFoodReviewApp.Models;
using LegitFoodReviewApp.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LegitFoodReviewApp.Controllers
{

    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class OwnerController : Controller
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public OwnerController(IOwnerRepository ownerRepository, ICountryRepository countryRepository ,IMapper mapper)
        {
            _ownerRepository = ownerRepository;
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        [HttpGet]  // Potreban kod za pravljenje ovog endpointa
        [ProducesResponseType(200, Type = typeof(IEnumerable<Owner>))]
        public IActionResult GetOwners() // GET celu listu Food-a
        {
            var owners = _mapper.Map<List<OwnerDto>>(_ownerRepository.GetOwners());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(owners);
        }

        [HttpGet("{ownerId}")]
        [ProducesResponseType(200, Type = typeof(Owner))]
        [ProducesResponseType(400)]
        public IActionResult GetOwner(int ownerId)  // GET jednog Food-a po Id
        {

            if (!_ownerRepository.OwnerExists(ownerId))
                return NotFound();

            var owner = _mapper.Map<OwnerDto>(_ownerRepository.GetOwner(ownerId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(owner);
        }


        [HttpGet("/{ownerId}/food")]
        [ProducesResponseType(200, Type = typeof(Owner))]
        [ProducesResponseType(400)]

        public IActionResult GetFoodByOwner(int ownerId)
        {
            if (!_ownerRepository.OwnerExists(ownerId))
            {  
                return NotFound();
            }

            var food = _mapper.Map<List<FoodDto>>
                (_ownerRepository.GetFoodByOwner(ownerId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(food);
        }


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateOwner([FromQuery] int countryId, [FromBody] OwnerDto ownerCreate)
        {
            if (ownerCreate == null)
                return BadRequest(ModelState);

            var owner = _ownerRepository.GetOwners()
                .Where(c => c.Name.Trim().ToUpper() == ownerCreate.Name.TrimEnd().ToUpper()).FirstOrDefault();

            if (owner != null)
            {
                ModelState.AddModelError("", "Vec postoji owner");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ownerMap = _mapper.Map<Owner>(ownerCreate);

            ownerMap.Country = _countryRepository.GetCountry(countryId); // ovo moze da se napise i u OwnerRepository

            if (!_ownerRepository.CreateOwner(ownerMap))
            {
                ModelState.AddModelError("", "Something when wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("uspesno napravljeno");
        }

        [HttpPut("{ownerId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateOwner(int ownerId, [FromBody] OwnerDto updatedOwner)
        {
            if (updatedOwner == null)
                return BadRequest(ModelState);

            if (ownerId != updatedOwner.Id)
                return BadRequest(ModelState);

            if (!_ownerRepository.OwnerExists(ownerId))
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ownerMap = _mapper.Map<Owner>(updatedOwner);

            if (!_ownerRepository.UpdateOwner(ownerMap))
            {
                ModelState.AddModelError("", "Something went wrong updating category");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{ownerId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteOwner(int ownerId)
        {
            if (!_ownerRepository.OwnerExists(ownerId))
            {
                return NotFound();
            }

            var ownerToDelete = _ownerRepository.GetOwner(ownerId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_ownerRepository.DeleteOwner(ownerToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting");
            }
            return NoContent();
        }
    }
}
