using DTO;
using DTO.Request;
using FirstProject.Models;
using FirstProject.Repositories.Interfaces;
using Moq;

namespace Handler.Tests
{
    public class DataHandlerTests
    {
        private readonly DataHandler _handler;
        private readonly Mock<IDataRepo> _repoMock;

        public DataHandlerTests()
        {
            _repoMock = new Mock<IDataRepo>();
            _handler = new DataHandler(_repoMock.Object);
        }

        // GetAllPersonDataAsync

        [Fact]
        public async Task GetAllPersonDataAsync_ReturnsDataList()
        {
            var datalist = new List<PersonData> { SampleEntity(1), SampleEntity(2) };
            _repoMock.Setup(r => r.GetAllDataAsync(It.IsAny<PersonDataFilterDto>())).ReturnsAsync(datalist);

            var result = (await _handler.GetAllPersonDataAsync(DefaultFilter())).ToList();

            Assert.NotNull(result);
            Assert.Equal(datalist[0].Id, result[0].PersonId);
        }

        [Fact]
        public async Task GetAllPersonDataAsync_ReturnsEmptyList()
        {
            _repoMock.Setup(r => r.GetAllDataAsync(It.IsAny<PersonDataFilterDto>())).ReturnsAsync(new List<PersonData>());

            var result = (await _handler.GetAllPersonDataAsync(DefaultFilter())).ToList();

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAllPersonDataAsync_RepoThrowsException()
        {
            _repoMock.Setup(r => r.GetAllDataAsync(It.IsAny<PersonDataFilterDto>())).ThrowsAsync(new Exception("Database error"));

            var ex = await Assert.ThrowsAsync<ApplicationException>(() => _handler.GetAllPersonDataAsync(DefaultFilter()));

            Assert.Contains("Failed to get person data list", ex.Message);
        }

        // GetPersonDataByIdAsync

        [Fact]
        public async Task GetPersonDataByIdAsync_ReturnsData()
        {
            var entity = SampleEntity(1);
            _repoMock.Setup(r => r.GetDataByIdAsync(1)).ReturnsAsync(entity);

            var result = await _handler.GetPersonDataByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(entity.Id, result.PersonId);
            Assert.Equal(entity.Name, result.PersonName);
        }

        [Fact]
        public async Task GetPersonDataByIdAsync_ReturnsNull()
        {
            _repoMock.Setup(r => r.GetDataByIdAsync(It.IsAny<int>())).ReturnsAsync((PersonData?)null);

            var result = await _handler.GetPersonDataByIdAsync(1);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetPersonDataByIdAsync_RepoThrowsException()
        {
            _repoMock.Setup(r => r.GetDataByIdAsync(It.IsAny<int>())).ThrowsAsync(new Exception("Database error"));

            var ex = await Assert.ThrowsAsync<ApplicationException>(() => _handler.GetPersonDataByIdAsync(1));

            Assert.Contains("Failed to get person data", ex.Message);
        }

        // CreatePersonDataAsync
        [Fact]
        public async Task CreatePersonDataAsync_ReturnsCreatedData()
        {
            var request = SampleCreateRequest();
            var entity = SampleEntity(1);
            _repoMock.Setup(r => r.CreateDataAsync(It.IsAny<PersonData>())).ReturnsAsync(entity);

            var result = await _handler.CreatePersonDataAsync(request);

            Assert.NotNull(result);
            Assert.Equal(entity.Id, result.PersonId);
            Assert.Equal(entity.Name, result.PersonName);
        }

        [Fact]
        public async Task CreatePersonDataAsync_RepoThrowsException()
        {
            var request = SampleCreateRequest();
            _repoMock.Setup(r => r.CreateDataAsync(It.IsAny<PersonData>())).ThrowsAsync(new Exception("Database error"));

            var ex = await Assert.ThrowsAsync<ApplicationException>(() => _handler.CreatePersonDataAsync(request));

            Assert.Contains("Failed to create person data", ex.Message);
        }

        // UpdatePersonDataAsync

        [Fact]
        public async Task UpdatePersonDataAsync_ReturnsUpdatedData()
        {
            var request = SampleUpdateRequest();
            var entity = SampleEntity(1);
            _repoMock.Setup(r => r.GetDataByIdAsync(1)).ReturnsAsync(entity);
            _repoMock.Setup(r => r.UpdateDataAsync(entity, request))
                .Callback<PersonData, UpdatePersonDataRequest>((e, dto) =>
                {
                    e.Name = dto.PersonName;
                    e.DateOfBirth = dto.PersonDoB;
                    e.HeightInFeet = dto.PersonHeight;
                    e.WeightInKg = dto.PersonWeight;
                    e.Gender = dto.PersonGender;
                    e.IsGraduated = dto.PersonIsGraduated;
                    e.MaritalStatus = dto.PersonMaritalStatus;  
                }).Returns(Task.CompletedTask);

            var result = await _handler.UpdatePersonDataAsync(1, request);

            Assert.NotNull(result);
            Assert.Equal(entity.Id, result.PersonId);
            Assert.Equal(entity.Name, result.PersonName);

            _repoMock.Verify(r => r.UpdateDataAsync(entity, request), Times.Once);
        }

        [Fact]
        public async Task UpdatePersonDataAsync_ReturnsNull()
        {
            var request = SampleUpdateRequest();
            _repoMock.Setup(r => r.GetDataByIdAsync(It.IsAny<int>())).ReturnsAsync((PersonData?)null);
            var result = await _handler.UpdatePersonDataAsync(1, request);
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdatePersonDataAsync_RepoThrowsException()
        {
            var request = SampleUpdateRequest();
            var entity = SampleEntity(1);
            _repoMock.Setup(r => r.GetDataByIdAsync(1)).ReturnsAsync(entity);
            _repoMock.Setup(r => r.UpdateDataAsync(entity, request)).ThrowsAsync(new Exception("Database error"));
            var ex = await Assert.ThrowsAsync<ApplicationException>(() => _handler.UpdatePersonDataAsync(1, request));
            Assert.Contains("Failed to update person data", ex.Message);
        }

        // DeletePersonDataAsync

        [Fact]
        public async Task DeletePersonDataAsync_ReturnsTrue()
        {
            var entity = SampleEntity(1);
            _repoMock.Setup(r => r.GetDataByIdAsync(1)).ReturnsAsync(entity);
            _repoMock.Setup(r => r.DeleteDataAsync(entity)).Returns(Task.CompletedTask);
            var result = await _handler.DeletePersonDataAsync(1);
            Assert.True(result);
            _repoMock.Verify(r => r.DeleteDataAsync(entity), Times.Once);
        }

        [Fact]
        public async Task DeletePersonDataAsync_ReturnsFalse()
        {
            _repoMock.Setup(r => r.GetDataByIdAsync(It.IsAny<int>())).ReturnsAsync((PersonData?)null);
            var result = await _handler.DeletePersonDataAsync(1);
            Assert.False(result);
        }

        [Fact]
        public async Task DeletePersonDataAsync_RepoThrowsException()
        {
            var entity = SampleEntity(1);
            _repoMock.Setup(r => r.GetDataByIdAsync(1)).ReturnsAsync(entity);
            _repoMock.Setup(r => r.DeleteDataAsync(entity)).ThrowsAsync(new Exception("Database error"));
            var ex = await Assert.ThrowsAsync<ApplicationException>(() => _handler.DeletePersonDataAsync(1));
            Assert.Contains("Failed to delete person data", ex.Message);
        }

        // Models

        private static PersonData SampleEntity(int id = 1) => new PersonData
        {
            Id = id,
            Name = "Dabananda Mitra",
            DateOfBirth = new DateOnly(2001, 8, 2),
            HeightInFeet = 5.75m,
            WeightInKg = 89,
            Gender = "Male",
            MaritalStatus = "Single",
            IsGraduated = true,
            IsDeleted = false
        };

        private static CreatePersonDataRequest SampleCreateRequest() => new CreatePersonDataRequest
        {
            PersonName = "Dabananda Mitra",
            PersonDoB = new DateOnly(2001, 8, 2),
            PersonHeight = 5.75m,
            PersonWeight = 89,
            PersonGender = "Male",
            PersonMaritalStatus = "Single",
            PersonIsGraduated = true
        };

        private static UpdatePersonDataRequest SampleUpdateRequest() => new UpdatePersonDataRequest
        {
            PersonName = "Suraiya Bilkis",
            PersonDoB = new DateOnly(2000, 4, 15),
            PersonHeight = 5.3m,
            PersonWeight = 55,
            PersonGender = "Female",
            PersonMaritalStatus = "Married",
            PersonIsGraduated = false
        };

        private static PersonDataFilterDto DefaultFilter() => new PersonDataFilterDto
        {
            PageNumber = 1,
            PageSize = 10
        };
    }
}
