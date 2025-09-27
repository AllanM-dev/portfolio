using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.DTOs.Experiences;
using Portfolio.Application.UseCases;

namespace Portfolio.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExperiencesController : ControllerBase
    {
        private readonly GetExperiences _getExperiences;
        private readonly GetExperienceById _getExperienceById;
        private readonly AddExperience _addExperience;
        private readonly UpdateExperience _updateExperience;
        private readonly DeleteExperience _deleteExperience;

        public ExperiencesController(
            GetExperiences getExperiences,
            GetExperienceById getExperienceById,
            AddExperience addExperience,
            UpdateExperience updateExperience,
            DeleteExperience deleteExperience)
        {
            _getExperiences = getExperiences;
            _getExperienceById = getExperienceById;
            _addExperience = addExperience;
            _updateExperience = updateExperience;
            _deleteExperience = deleteExperience;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ExperienceDto>> GetAll()
        {
            var experiences = _getExperiences.Execute();
            return Ok(experiences);
        }

        [HttpGet("{id}")]
        public ActionResult<ExperienceDto> GetById(int id)
        {
            var experience = _getExperienceById.Execute(id);
            if (experience == null)
                return NotFound();
            return Ok(experience.ToDto());
        }

        [HttpPost]
        [ProducesResponseType<ExperienceDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create([FromBody] CreateExperienceDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdExperience = dto.ToEntity();
            _addExperience.Execute(createdExperience);

            return CreatedAtAction(nameof(GetById), new { id = createdExperience.Id }, createdExperience.ToDto());
        }
    }
}
