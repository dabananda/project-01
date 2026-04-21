using DTO;
using DTO.Request;
using DTO.Response;

namespace Handler
{
    public interface IDataHandler
    {
        Task<IEnumerable<PersonDataResponse>> GetAllPersonDataAsync(PersonDataFilterDto dto);
        Task<PersonDataResponse?> GetPersonDataByIdAsync(int id);
        Task<PersonDataResponse> CreatePersonDataAsync(CreatePersonDataRequest dto);
        Task<PersonDataResponse?> UpdatePersonDataAsync(int id, UpdatePersonDataRequest dto);
        Task<bool> DeletePersonDataAsync(int id);
    }
}
