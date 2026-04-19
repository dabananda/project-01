using FirstProject.Models;

namespace FirstProject.Repositories.Interfaces
{
    public interface IDataRepo
    {
        Task<PersonData?> GetDataByIdAsync(int id);
        Task<List<PersonData>> GetAllDataAsync(string? name = null, string? gender = null, string? maritalStatus = null, bool? isGraduated = null, int pageNumber = 1, int pageSize = 10);
        Task<PersonData> CreateDataAsync(PersonData data);
        Task<PersonData?> UpdateDataAsync(int id, PersonData data);
        Task<bool> DeleteDataAsync(int id);
    }
}
