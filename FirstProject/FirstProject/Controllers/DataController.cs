using DTO;
using DTO.Request;
using Handler;
using Microsoft.AspNetCore.Mvc;

namespace FirstProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly IDataHandler _dataHandler;

        public DataController(IDataHandler dataHandler)
        {
            _dataHandler = dataHandler;
        }

        [HttpGet("GetAllPersonsData")]
        public async Task<IActionResult> GetAllPersonsData([FromQuery] PersonDataFilterDto dto)
        {
            return Ok(await _dataHandler.GetAllPersonDataAsync(dto));
        }

        [HttpGet("GetPersonDataById/{id}")]
        public async Task<IActionResult> GetPersonDataById([FromRoute] int id)
        {
            var resultDto = await _dataHandler.GetPersonDataByIdAsync(id);
            if (resultDto == null)
            {
                return NotFound($"Person data with id: {id} does not exist.");
            }
            return Ok(resultDto);
        }

        [HttpPost("CreatePersonData")]
        public async Task<IActionResult> CreatePersonData([FromBody] CreatePersonDataRequest dto)
        {
            var responseDto = await _dataHandler.CreatePersonDataAsync(dto);
            return Ok(new
            {
                message = "Data created successfully.",
                data = responseDto
            });
        }

        [HttpPut("UpdatePersonData/{id}")]
        public async Task<IActionResult> UpdatePersonData([FromRoute] int id, [FromBody] UpdatePersonDataRequest dto)
        {
            var responseDto = await _dataHandler.UpdatePersonDataAsync(id, dto);
            if (responseDto == null)
            {
                return NotFound($"Person data with id: {id} does not exist.");
            }
            return Ok(new
            {
                message = "Person Data updated successfully.",
                data = responseDto
            });
        }

        [HttpDelete("DeletePersonData/{id}")]
        public async Task<IActionResult> DeletePersonData([FromRoute] int id)
        {
            var response = await _dataHandler.DeletePersonDataAsync(id);
            if (response == false)
            {
                return NotFound($"Person data with id: {id} does not exist.");
            }
            return Ok(new
            {
                message = "Person data deleted successfully."
            });
        }
    }
}
