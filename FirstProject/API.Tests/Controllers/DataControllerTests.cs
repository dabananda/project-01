using Aggregator.Enums;
using DTO;
using DTO.Request;
using DTO.Response;
using FirstProject.Controllers;
using Handler;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace API.Tests.Controllers
{
    public class DataControllerTests
    {
        private readonly Mock<IDataHandler> _handlerMock;
        private readonly DataController _controller;

        public DataControllerTests()
        {
            _handlerMock = new Mock<IDataHandler>();
            _controller = new DataController(_handlerMock.Object);
        }

        [Fact]
        public async Task GetAllPersonsData_ReturnsOkResult_WithListOfData()
        {
            var list = new List<PersonDataResponse> { SampleResponse(1), SampleResponse(2) };
            _handlerMock.Setup(h => h.GetAllPersonDataAsync(It.IsAny<PersonDataFilterDto>())).ReturnsAsync(list);
            var result = await _controller.GetAllPersonsData(new PersonDataFilterDto()) as OkObjectResult;
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(list, result.Value);
        }

        [Fact]
        public async Task GetAllPersonsData_ReturnsOkResult_WithEmptyList()
        {
            var list = new List<PersonDataResponse>();
            _handlerMock.Setup(h => h.GetAllPersonDataAsync(It.IsAny<PersonDataFilterDto>())).ReturnsAsync(list);
            var result = await _controller.GetAllPersonsData(new PersonDataFilterDto()) as OkObjectResult;
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(list, result.Value);
        }

        [Fact]
        public async Task GetPersonDataById_ReturnsOkResult_WithData()
        {
            var response = SampleResponse(1);
            _handlerMock.Setup(h => h.GetPersonDataByIdAsync(1)).ReturnsAsync(response);
            var result = await _controller.GetPersonDataById(1) as OkObjectResult;
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(response, result.Value);
        }

        [Fact]
        public async Task GetPersonDataById_ReturnsNotFoundResult_WithInvalidId()
        {
            _handlerMock.Setup(h => h.GetPersonDataByIdAsync(1)).ReturnsAsync((PersonDataResponse?)null);
            var result = await _controller.GetPersonDataById(1) as NotFoundObjectResult;
            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
            Assert.Equal("Person data with id: 1 does not exist.", result.Value);
        }

        [Fact]
        public async Task CreatePersonData_ReturnsOkResult_WithMessage()
        {
            var request = SampleCreateRequest();
            var response = SampleResponse(1);
            
            _handlerMock.Setup(h => h.CreatePersonDataAsync(request)).ReturnsAsync(response);
            
            var result = await _controller.CreatePersonData(request) as OkObjectResult;
            
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            
            var value = result.Value;
            var messageProp = value.GetType().GetProperty("message");
            var dataProp = value.GetType().GetProperty("data");

            Assert.NotNull(value);
            Assert.NotNull(messageProp);
            Assert.NotNull(dataProp);
            Assert.Equal("Data created successfully.", messageProp.GetValue(value));
            Assert.Equal(response, dataProp.GetValue(value));
        }

        [Fact]
        public async Task UpdatePersonData_ReturnsOkResult_WithMessage()
        {
            var response = SampleResponse(1);

            _handlerMock.Setup(h => h.UpdatePersonDataAsync(1, It.IsAny<UpdatePersonDataRequest>())).ReturnsAsync(response);

            var result = await _controller.UpdatePersonData(1, SampleUpdateRequest()) as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            var value = result.Value;
            var messageProp = value.GetType().GetProperty("message");
            var dataProp = value.GetType().GetProperty("data");

            Assert.NotNull(value);
            Assert.NotNull(messageProp);
            Assert.NotNull(dataProp);
            Assert.Equal(response, dataProp.GetValue(value));
            Assert.Equal("Person Data updated successfully.", messageProp.GetValue(value));
        }

        [Fact]
        public async Task UpdatePersonData_ReturnsNotFoundResult_WithInvalidId()
        {
            _handlerMock.Setup(h => h.UpdatePersonDataAsync(1, It.IsAny<UpdatePersonDataRequest>())).ReturnsAsync((PersonDataResponse?)null);
            var result = await _controller.UpdatePersonData(1, SampleUpdateRequest()) as NotFoundObjectResult;
            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
            Assert.Equal("Person data with id: 1 does not exist.", result.Value);
        }

        [Fact]
        public async Task DeletePersonData_ReturnsOkResult_WithMessage()
        {
            _handlerMock.Setup(h => h.DeletePersonDataAsync(1)).ReturnsAsync(true);
            var result = await _controller.DeletePersonData(1) as OkObjectResult;
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var value = result.Value;
            var messageProp = value.GetType().GetProperty("message");
            Assert.NotNull(value);
            Assert.NotNull(messageProp);
            Assert.Equal("Person data deleted successfully.", messageProp.GetValue(result.Value));
        }

        [Fact]
        public async Task DeletePersonData_ReturnsNotFoundResult_WithInvalidId()
        {
            _handlerMock.Setup(h => h.DeletePersonDataAsync(1)).ReturnsAsync(false);
            var result = await _controller.DeletePersonData(1) as NotFoundObjectResult;
            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
            Assert.Equal("Person data with id: 1 does not exist.", result.Value);
        }

        // DTOs
        private static PersonDataResponse SampleResponse(int id = 1) => new PersonDataResponse
        {
            PersonId = id,
            PersonName = "Dabananda Mitra",
            PersonDoB = new DateOnly(2001, 8, 2),
            PersonHeight = 5.75m,
            PersonWeight = 89,
            PersonGender = Gender.Male,
            PersonMaritalStatus = MaritalStatus.Single,
            PersonIsGraduated = true
        };

        private static CreatePersonDataRequest SampleCreateRequest() => new CreatePersonDataRequest
        {
            PersonName = "Dabananda Mitra",
            PersonDoB = new DateOnly(2001, 8, 2),
            PersonHeight = 5.75m,
            PersonWeight = 89,
            PersonGender = Gender.Male,
            PersonMaritalStatus = MaritalStatus.Single,
            PersonIsGraduated = true
        };

        private static UpdatePersonDataRequest SampleUpdateRequest() => new UpdatePersonDataRequest
        {
            PersonName = "Suraiya Bilkis",
            PersonDoB = new DateOnly(2000, 4, 15),
            PersonHeight = 5.3m,
            PersonWeight = 55,
            PersonGender = Gender.Female,
            PersonMaritalStatus = MaritalStatus.Single,
            PersonIsGraduated = false
        };
    }
}
