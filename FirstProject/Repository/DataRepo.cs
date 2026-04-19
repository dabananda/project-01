using FirstProject.Data;
using FirstProject.Models;
using FirstProject.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FirstProject.Repositories
{
    public class DataRepo : IDataRepo
    {
        private readonly AppDbContext _context;

        public DataRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PersonData> CreateDataAsync(PersonData data)
        {
            await _context.PersonDatas.AddAsync(data);
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<bool> DeleteDataAsync(int id)
        {
            var personData = await _context.PersonDatas.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
            if (personData == null) return false;
            personData.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<PersonData>> GetAllDataAsync(string? name = null, string? gender = null, string? maritalStatus = null, bool? isGraduated = null, int pageNumber = 1, int pageSize = 10)
        {
            var datas = _context.PersonDatas.AsNoTracking().Where(x => x.IsDeleted == false).OrderByDescending(x => x.Id).AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                datas = datas.Where(x => x.Name.Contains(name));
            }

            if (!string.IsNullOrEmpty(gender))
            {
                datas = datas.Where(x => x.Gender == gender);
            }

            if (!string.IsNullOrEmpty(maritalStatus))
            {
                datas = datas.Where(x => x.MaritalStatus == maritalStatus);
            }

            if (isGraduated.HasValue)
            {
                datas = datas.Where(x => x.IsGraduated == isGraduated.Value);
            }

            var skipRes = (pageNumber - 1) * pageSize;
            return await datas.Skip(skipRes).Take(pageSize).ToListAsync();
        }

        public async Task<PersonData?> GetDataByIdAsync(int id)
        {
            var personData = await _context.PersonDatas.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
            if (personData == null) return null;
            return personData;
        }

        public async Task<PersonData?> UpdateDataAsync(int id, PersonData data)
        {
            var personData = await _context.PersonDatas.FirstOrDefaultAsync(x => x.Id == id);
            if (personData == null) return null;
            personData.Name = data.Name;
            personData.DateOfBirth = data.DateOfBirth;
            personData.HeightInFeet = data.HeightInFeet;
            personData.WeightInKg = data.WeightInKg;
            personData.Gender = data.Gender;
            personData.MaritalStatus = data.MaritalStatus;
            personData.IsGraduated = data.IsGraduated;
            await _context.SaveChangesAsync();
            return personData;
        }
    }
}
