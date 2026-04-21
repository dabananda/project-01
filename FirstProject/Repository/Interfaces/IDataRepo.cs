using FirstProject.Models;

namespace FirstProject.Repositories.Interfaces
{
    public interface IDataRepo
    {
        Task<PersonData?> GetDataByIdAsync(int id);
        Task<List<PersonData>> GetAllDataAsync(string? name, string? gender, string? maritalStatus, bool? isGraduated, int pageNumber, int pageSize);
        Task<PersonData> CreateDataAsync(PersonData data);
        Task UpdateDataAsync(PersonData entity);
        Task DeleteDataAsync(PersonData data);
    }
}
