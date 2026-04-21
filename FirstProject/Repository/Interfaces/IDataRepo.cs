using DTO;
using DTO.Request;
using FirstProject.Models;

namespace FirstProject.Repositories.Interfaces
{
    public interface IDataRepo
    {
        Task<PersonData?> GetDataByIdAsync(int id);
        Task<List<PersonData>> GetAllDataAsync(PersonDataFilterDto dto);
        Task<PersonData> CreateDataAsync(PersonData data);
        Task UpdateDataAsync(PersonData entity, UpdatePersonDataRequest dto);
        Task DeleteDataAsync(PersonData data);
    }
}
