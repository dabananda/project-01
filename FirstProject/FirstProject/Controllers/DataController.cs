using FirstProject.DTOs.Data;
using FirstProject.Models;
using FirstProject.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FirstProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly IDataRepo _repo;

        public DataController(IDataRepo repo)
        {
            _repo = repo;
        }

        [HttpGet("GetAllPersonsData")]
        public async Task<IActionResult> GetAllPersonsData([FromQuery] string? name, [FromQuery] string? gender, [FromQuery] string? maritalStatus, [FromQuery] bool? isGraduated, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var datas = await _repo.GetAllDataAsync(name, gender, maritalStatus, isGraduated, pageNumber, pageSize);
                var dtos = new List<PersonDataDto>();
                foreach (var data in datas)
                {
                    dtos.Add(new PersonDataDto
                    {
                        Id = data.Id,
                        Name = data.Name,
                        DateOfBirth = data.DateOfBirth,
                        HeightInFeet = data.HeightInFeet,
                        WeightInKg = data.WeightInKg,
                        Gender = data.Gender,
                        MaritalStatus = data.MaritalStatus,
                        IsGraduated = data.IsGraduated
                    });
                }
                return Ok(dtos);
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
                var data = await _repo.GetDataByIdAsync(id);
                if (data == null)
                {
                    return NotFound($"Person data with id: {id} does not exist.");
                }
                var dto = new PersonDataDto
                {
                    Id = data.Id,
                    Name = data.Name,
                    DateOfBirth = data.DateOfBirth,
                    HeightInFeet = data.HeightInFeet,
                    WeightInKg = data.WeightInKg,
                    Gender = data.Gender,
                    MaritalStatus = data.MaritalStatus,
                    IsGraduated = data.IsGraduated
                };
                return Ok(dto);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while getting person data", ex);
            }
        }

        [HttpPost("CreatePersonData")]
        public async Task<IActionResult> CreatePersonData([FromBody] CreatePersonDataDto dto)
        {
            try
            {
                var domain = new PersonData
                {
                    Name = dto.Name,
                    DateOfBirth = dto.DateOfBirth,
                    HeightInFeet = dto.HeightInFeet,
                    WeightInKg = dto.WeightInKg,
                    Gender = dto.Gender,
                    MaritalStatus = dto.MaritalStatus,
                    IsGraduated = dto.IsGraduated
                };
                var result = await _repo.CreateDataAsync(domain);
                return Ok(new
                {
                    message = "Data created successfully."
                });
            }
            catch (Exception ex)
            {
                throw new Exception("Error while creating person data", ex);
            }
        }

        [HttpPut("UpdatePersonData/{id}")]
        public async Task<IActionResult> UpdatePersonData([FromRoute] int id, [FromBody] UpdatePersonDataDto dto)
        {
            try
            {
                var domain = new PersonData
                {
                    Name = dto.Name,
                    DateOfBirth = dto.DateOfBirth,
                    HeightInFeet = dto.HeightInFeet,
                    WeightInKg = dto.WeightInKg,
                    Gender = dto.Gender.ToString(),
                    MaritalStatus = dto.MaritalStatus.ToString(),
                    IsGraduated = dto.IsGraduated
                };
                var result = await _repo.UpdateDataAsync(id, domain);
                if (result == null)
                {
                    return NotFound($"Person data with id: {id} does not exist.");
                }
                return Ok(new
                {
                    message = "Person Data updated successfully."
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
                var result = await _repo.DeleteDataAsync(id);
                if (result == false)
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
