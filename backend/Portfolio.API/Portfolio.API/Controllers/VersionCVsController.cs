using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.DTOs.VersionCvs;
using Portfolio.Application.UseCases.VersionCVs;

namespace Portfolio.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VersionCVsController : ControllerBase
    {
        private readonly AddVersionCV _addVersionCV;
        private readonly GetVersionsByUser _getVersionsByUser;

        public VersionCVsController(AddVersionCV addVersionCV, GetVersionsByUser getVersionsByUser)
        {
            _addVersionCV = addVersionCV;
            _getVersionsByUser = getVersionsByUser;
        }

        [HttpGet("user/{userId}")]
        public ActionResult<IEnumerable<VersionCVDto>> GetByUser(Guid userId)
        {
            var versions = _getVersionsByUser.Execute(userId);
            return Ok(versions);
        }

        [HttpPost]
        [ProducesResponseType<VersionCVDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create([FromBody] CreateVersionCVDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var createdVersion = dto.ToEntity();
            _addVersionCV.Execute(createdVersion);
            return CreatedAtAction(nameof(GetByUser), new { userId = createdVersion.UserId }, createdVersion.ToDto());
        }
    }
}
