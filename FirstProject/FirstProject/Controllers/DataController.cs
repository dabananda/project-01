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
            try
            {
                var response = await _dataHandler.GetAllPersonDataAsync(dto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while getting all person data", ex);
            }
        }

        [HttpGet("GetPersonDataById/{id}")]
        public async Task<IActionResult> GetPersonDataById([FromRoute] int id)
        {
            try
            {
                var resultDto = await _dataHandler.GetPersonDataByIdAsync(id);
                if (resultDto == null)
                {
                    return NotFound($"Person data with id: {id} does not exist.");
                }
                return Ok(resultDto);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while getting person data", ex);
            }
        }

        [HttpPost("CreatePersonData")]
        public async Task<IActionResult> CreatePersonData([FromBody] CreatePersonDataRequest dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var responseDto = await _dataHandler.CreatePersonDataAsync(dto);
                return Ok(new
                {
                    message = "Data created successfully.",
                    data = responseDto
                });
            }
            catch (Exception ex)
            {
                throw new Exception("Error while creating person data", ex);
            }
        }

        [HttpPut("UpdatePersonData/{id}")]
        public async Task<IActionResult> UpdatePersonData([FromRoute] int id, [FromBody] UpdatePersonDataRequest dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
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
            catch (Exception ex)
            {
                throw new Exception("Error while updaing person data", ex);
            }
        }

        [HttpDelete("DeletePersonData/{id}")]
        public async Task<IActionResult> DeletePersonData([FromRoute] int id)
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception("Error while deleting person data", ex);
            }
        }
    }
}
