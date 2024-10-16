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

    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper) 
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]  // Potreban kod za pravljenje ovog endpointa
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        public IActionResult GetCategories() // GET celu listu Food-a
        {
            var categories = _mapper.Map<List<CategoryDto>>(_categoryRepository.GetCategories());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(categories);
        }

        [HttpGet("{categoryId}")]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(400)]
        public IActionResult GetCategory(int categoryId)  // GET jednog Food-a po Id
        {

            if (!_categoryRepository.CategoryExists(categoryId))
                return NotFound();

            var category = _mapper.Map<CategoryDto>(_categoryRepository.GetCategory(categoryId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(category);
        }

        [HttpGet("food/{categoryId}")]  // food se gleda kao kolekcija clanova, dok {} se gleda kao jedan element
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        [ProducesResponseType(400)]

        public IActionResult GetFoodByCategoryId(int categoryId)
        {
            var food = _mapper.Map<List<FoodDto>>(_categoryRepository.GetFoodByCategory(categoryId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(food);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCategory([FromBody] CategoryDto categoryCreate)
        {
            if(categoryCreate == null)
                return BadRequest(ModelState);

            var category = _categoryRepository.GetCategories()
                .Where(c=>c.Name.Trim().ToUpper() == categoryCreate.Name.TrimEnd().ToUpper()).FirstOrDefault();

            if(category != null)
            {
                ModelState.AddModelError("", "Vec postoji kategorija");
                return StatusCode(422, ModelState);
            }

            if(!ModelState.IsValid) 
                return BadRequest(ModelState);

            var categoryMap = _mapper.Map<Category>(categoryCreate);

            if(!_categoryRepository.CreateCategory(categoryMap))
            {
                ModelState.AddModelError("", "Something when wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("uspesno napravljeno");
        }

        [HttpPut("{categoryId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCategory(int categoryId, [FromBody] CategoryDto updatedCategory)
        {
            if (updatedCategory == null)
                return BadRequest(ModelState);

            if (categoryId != updatedCategory.Id)
                return BadRequest(ModelState);

            if (!_categoryRepository.CategoryExists(categoryId))
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoryMap = _mapper.Map<Category>(updatedCategory);

            if (!_categoryRepository.UpdateCategory(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong updating category");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{categoryId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCategory(int categoryId) 
        {
            if (!_categoryRepository.CategoryExists(categoryId))
            {
                return NotFound();
            }

            var categoryToDelete=_categoryRepository.GetCategory(categoryId);

            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            if(!_categoryRepository.DeleteCategory(categoryToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting");
            }
            return NoContent();
        }

    }
}
