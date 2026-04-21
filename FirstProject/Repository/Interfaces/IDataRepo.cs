using Aggregator.Enums;
using FirstProject.Models;

namespace FirstProject.Repositories.Interfaces
{
    public interface IDataRepo
    {
        Task<PersonData?> GetDataByIdAsync(int id);
        Task<List<PersonData>> GetAllDataAsync(string? name, Gender? gender, MaritalStatus? maritalStatus, bool? isGraduated, int pageNumber, int pageSize);
        Task<PersonData> CreateDataAsync(PersonData data);
        Task UpdateDataAsync(PersonData entity);
        Task DeleteDataAsync(PersonData data);
    }
}
