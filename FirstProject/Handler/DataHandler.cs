using Aggregator.Mappings;
using DTO;
using DTO.Request;
using DTO.Response;
using FirstProject.Repositories.Interfaces;

namespace Handler
{
    public class DataHandler : IDataHandler
    {
        private readonly IDataRepo _repo;

        public DataHandler(IDataRepo repo)
        {
            _repo = repo;
        }

        public async Task<PersonDataResponse> CreatePersonDataAsync(CreatePersonDataRequest dto)
        {
            try
            {
                var entity = PersonDataMapper.ToEntity(dto);
                var createdEntity = await _repo.CreateDataAsync(entity);
                return PersonDataMapper.ToDto(createdEntity);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to create person data", ex);
            }
        }

        public async Task<bool> DeletePersonDataAsync(int id)
        {
            try
            {
                var result = await _repo.GetDataByIdAsync(id);
                if (result == null) return false;
                await _repo.DeleteDataAsync(result);
                return true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to delete person data", ex);
            }
        }

        public async Task<IEnumerable<PersonDataResponse>> GetAllPersonDataAsync(PersonDataFilterDto dto)
        {
            try
            {
                var entities = await _repo.GetAllDataAsync(dto.Name, dto.Gender, dto.MaritalStatus, dto.IsGraduated, dto.PageNumber, dto.PageSize);
                return PersonDataMapper.ToDtoList(entities);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to get person data list", ex);
            }
        }

        public async Task<PersonDataResponse?> GetPersonDataByIdAsync(int id)
        {
            try
            {
                var result = await _repo.GetDataByIdAsync(id);
                if (result == null) return null;
                return PersonDataMapper.ToDto(result);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to get person data", ex);
            }
        }

        public async Task<PersonDataResponse?> UpdatePersonDataAsync(int id, UpdatePersonDataRequest dto)
        {
            try
            {
                var entity = await _repo.GetDataByIdAsync(id);
                if (entity == null) return null;
                entity.Name = dto.PersonName;
                entity.DateOfBirth = dto.PersonDoB;
                entity.HeightInFeet = dto.PersonHeight;
                entity.WeightInKg = dto.PersonWeight;
                entity.Gender = dto.PersonGender;
                entity.MaritalStatus = dto.PersonMaritalStatus;
                entity.IsGraduated = dto.PersonIsGraduated;
                await _repo.UpdateDataAsync(entity);
                return PersonDataMapper.ToDto(entity);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to upate person data", ex);
            }
        }
    }
}
