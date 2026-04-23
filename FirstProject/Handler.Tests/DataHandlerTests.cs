using Aggregator.Enums;
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

        [Fact]
        public async Task GetAllPersonDataAsync_RepoReturnsData_ReturnsDataList()
        {
            var datalist = new List<PersonData> { SampleEntity(1), SampleEntity(2) };
            _repoMock.Setup(r => r.GetAllDataAsync(
                It.IsAny<string?>(),
                It.IsAny<Gender?>(),
                It.IsAny<MaritalStatus?>(),
                It.IsAny<bool?>(),
                It.IsAny<int>(),
                It.IsAny<int>())).ReturnsAsync(datalist);

            var result = (await _handler.GetAllPersonDataAsync(DefaultFilter())).ToList();

            Assert.NotNull(result);
            Assert.Equal(datalist[0].Id, result[0].PersonId);
        }

        [Fact]
        public async Task GetAllPersonDataAsync_RepoIsEmpty_ReturnsEmptyList()
        {
            _repoMock.Setup(r => r.GetAllDataAsync(
                It.IsAny<string?>(),
                It.IsAny<Gender?>(),
                It.IsAny<MaritalStatus?>(),
                It.IsAny<bool?>(),
                It.IsAny<int>(),
                It.IsAny<int>())).ReturnsAsync(new List<PersonData>());

            var result = (await _handler.GetAllPersonDataAsync(DefaultFilter())).ToList();

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAllPersonDataAsync_RepoThrowsException_ThrowsApplicationException()
        {
            _repoMock.Setup(r => r.GetAllDataAsync(
                It.IsAny<string?>(),
                It.IsAny<Gender?>(),
                It.IsAny<MaritalStatus?>(),
                It.IsAny<bool?>(),
                It.IsAny<int>(),
                It.IsAny<int>())).ThrowsAsync(new Exception("Database error"));

            var ex = await Assert.ThrowsAsync<ApplicationException>(() => _handler.GetAllPersonDataAsync(DefaultFilter()));

            Assert.Contains("Failed to get person data list", ex.Message);
        }

        [Fact]
        public async Task GetPersonDataByIdAsync_IdExists_ReturnsData()
        {
            var entity = SampleEntity(1);
            _repoMock.Setup(r => r.GetDataByIdAsync(1)).ReturnsAsync(entity);

            var result = await _handler.GetPersonDataByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(entity.Id, result.PersonId);
            Assert.Equal(entity.Name, result.PersonName);
        }

        [Fact]
        public async Task GetPersonDataByIdAsync_IdDoesNotExist_ReturnsNull()
        {
            _repoMock.Setup(r => r.GetDataByIdAsync(It.IsAny<int>())).ReturnsAsync((PersonData?)null);

            var result = await _handler.GetPersonDataByIdAsync(1);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetPersonDataByIdAsync_RepoThrowsException_ThrowsApplicationException()
        {
            _repoMock.Setup(r => r.GetDataByIdAsync(It.IsAny<int>())).ThrowsAsync(new Exception("Database error"));

            var ex = await Assert.ThrowsAsync<ApplicationException>(() => _handler.GetPersonDataByIdAsync(1));

            Assert.Contains("Failed to get person data", ex.Message);
        }

        [Fact]
        public async Task CreatePersonDataAsync_ValidRequest_ReturnsCreatedData()
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
        public async Task CreatePersonDataAsync_RepoThrowsException_ThrowsApplicationException()
        {
            var request = SampleCreateRequest();
            _repoMock.Setup(r => r.CreateDataAsync(It.IsAny<PersonData>())).ThrowsAsync(new Exception("Database error"));

            var ex = await Assert.ThrowsAsync<ApplicationException>(() => _handler.CreatePersonDataAsync(request));

            Assert.Contains("Failed to create person data", ex.Message);
        }

        [Fact]
        public async Task UpdatePersonDataAsync_IdExists_ReturnsUpdatedData()
        {
            var request = SampleUpdateRequest();
            var entity = SampleEntity(1);
            _repoMock.Setup(r => r.GetDataByIdAsync(1)).ReturnsAsync(entity);
            _repoMock.Setup(r => r.UpdateDataAsync(entity))
                .Callback<PersonData>(e =>
                {
                    e.Name = request.PersonName;
                    e.DateOfBirth = request.PersonDoB;
                    e.HeightInFeet = request.PersonHeight;
                    e.WeightInKg = request.PersonWeight;
                    e.Gender = request.PersonGender;
                    e.IsGraduated = request.PersonIsGraduated;
                    e.MaritalStatus = request.PersonMaritalStatus;
                }).Returns(Task.CompletedTask);

            var result = await _handler.UpdatePersonDataAsync(1, request);

            Assert.NotNull(result);
            Assert.Equal(entity.Id, result.PersonId);
            Assert.Equal(entity.Name, result.PersonName);

            _repoMock.Verify(r => r.UpdateDataAsync(entity), Times.Once);
        }

        [Fact]
        public async Task UpdatePersonDataAsync_IdDoesNotExist_ReturnsNull()
        {
            var request = SampleUpdateRequest();
            _repoMock.Setup(r => r.GetDataByIdAsync(It.IsAny<int>())).ReturnsAsync((PersonData?)null);
            var result = await _handler.UpdatePersonDataAsync(1, request);
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdatePersonDataAsync_RepoThrowsException_ThrowsApplicationException()
        {
            var request = SampleUpdateRequest();
            var entity = SampleEntity(1);
            _repoMock.Setup(r => r.GetDataByIdAsync(1)).ReturnsAsync(entity);
            _repoMock.Setup(r => r.UpdateDataAsync(entity)).ThrowsAsync(new Exception("Database error"));
            var ex = await Assert.ThrowsAsync<ApplicationException>(() => _handler.UpdatePersonDataAsync(1, request));
            Assert.Contains("Failed to update person data", ex.Message);
        }

        [Fact]
        public async Task DeletePersonDataAsync_IdExists_ReturnsTrue()
        {
            var entity = SampleEntity(1);
            _repoMock.Setup(r => r.GetDataByIdAsync(1)).ReturnsAsync(entity);
            _repoMock.Setup(r => r.DeleteDataAsync(entity)).Returns(Task.CompletedTask);
            var result = await _handler.DeletePersonDataAsync(1);
            Assert.True(result);
            _repoMock.Verify(r => r.DeleteDataAsync(entity), Times.Once);
        }

        [Fact]
        public async Task DeletePersonDataAsync_IdDoesNotExist_ReturnsFalse()
        {
            _repoMock.Setup(r => r.GetDataByIdAsync(It.IsAny<int>())).ReturnsAsync((PersonData?)null);
            var result = await _handler.DeletePersonDataAsync(1);
            Assert.False(result);
        }

        [Fact]
        public async Task DeletePersonDataAsync_RepoThrowsException_ThrowsApplicationException()
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
            Gender = Gender.Male,
            MaritalStatus = MaritalStatus.Single,
            IsGraduated = true,
            IsDeleted = false
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
            PersonMaritalStatus = MaritalStatus.Married,
            PersonIsGraduated = false
        };

        private static PersonDataFilterDto DefaultFilter() => new PersonDataFilterDto
        {
            PageNumber = 1,
            PageSize = 10
        };
    }
}
