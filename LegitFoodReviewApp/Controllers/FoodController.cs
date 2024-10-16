using AutoMapper;
using LegitFoodReviewApp.Dto;
using LegitFoodReviewApp.Interfaces;
using LegitFoodReviewApp.Models;
using LegitFoodReviewApp.Repository;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace LegitFoodReviewApp.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class FoodController : Controller
    {
        private readonly IFoodRepository _foodRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IOwnerRepository _ownerRepository;
        private readonly IMapper _mapper;

        public FoodController(IFoodRepository foodRepository, 
            IOwnerRepository ownerRepository, 
            IReviewRepository reviewRepository, 
            IMapper mapper) {

            _foodRepository = foodRepository;
            _ownerRepository = ownerRepository;
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }
        [HttpGet]  // Potreban kod za pravljenje ovog endpointa
        [ProducesResponseType(200, Type=typeof(IEnumerable<Food>))]
        public IActionResult GetFood() // GET celu listu Food-a
        {
            var food=_mapper.Map<List<FoodDto>>(_foodRepository.GetFood());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

        return Ok(food);
        }

        [HttpGet("{foodId}")]  
        [ProducesResponseType(200, Type = typeof(Food))] 
        [ProducesResponseType(400)]
        public IActionResult GetFoodSingle(int foodId)  // GET jednog Food-a po Id
        {  
        
            if(!_foodRepository.FoodExists(foodId))
                return NotFound();

            var food= _mapper.Map<FoodDto>(_foodRepository.GetFoodSingle(foodId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(food);
        }

        [HttpGet("{foodId}/rating")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]

        public IActionResult GetFoodRating(int foodId)  // GET jednog raiting-a po foodId
        {
            if (!_foodRepository.FoodExists(foodId))
                return NotFound();

            var rating= _foodRepository.GetFoodRating(foodId);

            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(rating);

        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateFood([FromQuery] int catId, [FromQuery] int ownerId, [FromBody] FoodDto foodCreate)
        {
            if (foodCreate == null)
                return BadRequest(ModelState);

            var food = _foodRepository.GetFood()
                .Where(c => c.Name.Trim().ToUpper() == foodCreate.Name.TrimEnd().ToUpper()).FirstOrDefault();

            if (food != null)
            {
                ModelState.AddModelError("", "Vec postoji ova hrana");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var foodMap = _mapper.Map<Food>(foodCreate);

            if (!_foodRepository.CreateFood(ownerId, catId, foodMap))
            {
                ModelState.AddModelError("", "Something when wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("uspesno napravljeno");
        }

        [HttpPut("{foodId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateFood(int foodId, 
            [FromQuery] int catId, 
            [FromQuery] int owenrId, 
            [FromBody] FoodDto updatedFood)
        {
            if (updatedFood == null)
                return BadRequest(ModelState);

            if (foodId != updatedFood.Id)
                return BadRequest(ModelState);

            if (!_foodRepository.FoodExists(foodId))
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var foodMap = _mapper.Map<Food>(updatedFood);

            if (!_foodRepository.UpdateFood(foodMap, catId, owenrId))
            {
                ModelState.AddModelError("", "Something went wrong updating category");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{foodId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteFood(int foodId)
        {
            if (!_foodRepository.FoodExists(foodId))
            {
                return NotFound();
            }

            var foodToDelete = _foodRepository.GetFoodSingle(foodId);
            var reviewsToDelete = _reviewRepository.GetReviewsOfAFood(foodId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if(!_reviewRepository.DeleteReviews(reviewsToDelete.ToList()))
            {
                ModelState.AddModelError("", "Something went wrong while deleting reviews");
            }

            if (!_foodRepository.DeleteFood(foodToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting");
            }
            return NoContent();
        }

    }
}
