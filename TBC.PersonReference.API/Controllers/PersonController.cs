using Microsoft.AspNetCore.Mvc;
using TBC.PersonReference.Application.Models.Request;
using TBC.PersonReference.Application.Services.PersonService;

namespace TBC.PersonReference.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPerson(int id)
        {
            var result = await _personService.GetPersonByIdAsync(id);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> SearchPersons([FromQuery] SearchPersonRequest request)
        {
            var result = await _personService.SearchPersonsAsync(request);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddPerson([FromBody] AddPersonRequest request)
        {
            var result = await _personService.AddPersonAsync(request);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePerson([FromBody] UpdatePersonRequest request, int id)
        {
            var result = await _personService.UpdatePersonAsync(request, id);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            var result = await _personService.DeletePersonAsync(id);

            return Ok($"Person with ID - {id} has been deleted");
        }

        [HttpPost("{id}/upload-image")]
        public async Task<IActionResult> UploadImage(int id, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest(new { Error = "File is required" });

            var result = await _personService.UploadImageAsync(id, file);

            if (!result.Success)
                return NotFound(new { Error = result.ErrorMessage });

            return Ok(result.FilePath);
        }

        [HttpPost("{id}/related-persons")]
        public async Task<IActionResult> AddRelatedPerson(int id, [FromBody] AddRelatedPersonRequest request)
        {
            var result = await _personService.AddRelatedPersonAsync(id, request.RelatedPersonId, request.RelationType);

            return Ok("Person relation has been added");
        }

        [HttpDelete("{id}/related-persons/{relatedPersonId}")]
        public async Task<IActionResult> RemoveRelatedPerson(int id, int relatedPersonId)
        {
            var result = await _personService.RemoveRelatedPersonAsync(id, relatedPersonId);

            return Ok("Person relation has been removed");
        }

        [HttpGet("{id}/related-persons/report")]
        public async Task<IActionResult> GetRelatedPersonsReport(int id)
        {
            var result = await _personService.GetRelatedPersonReportAsync(id);

            return Ok(result);
        }
    }
}
