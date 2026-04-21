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
                var response = await _repo.GetAllDataAsync(dto);
                return PersonDataMapper.ToDtoList(response);
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
                var result = await _repo.GetDataByIdAsync(id);
                if (result == null) return null;
                await _repo.UpdateDataAsync(result, dto);
                return PersonDataMapper.ToDto(result);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to upate person data", ex);
            }
        }
    }
}
