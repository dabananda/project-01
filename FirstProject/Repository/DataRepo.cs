using Aggregator.Enums;
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
            try
            {
                await _context.PersonDatas.AddAsync(data);
                await _context.SaveChangesAsync();
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to create new person data on database", ex);
            }
        }

        public async Task DeleteDataAsync(PersonData data)
        {
            try
            {
                data.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete person data on database", ex);
            }
        }

        public async Task<List<PersonData>> GetAllDataAsync(string? name, Gender? gender, MaritalStatus? maritalStatus, bool? isGraduated, int pageNumber, int pageSize)
        {
            try
            {
                var datas = _context.PersonDatas.AsNoTracking().Where(x => x.IsDeleted == false).OrderByDescending(x => x.Id).AsQueryable();

                if (!string.IsNullOrEmpty(name))
                {
                    datas = datas.Where(x => x.Name.Contains(name));
                }

                if (gender.HasValue)
                {
                    datas = datas.Where(x => x.Gender == gender);
                }

                if (maritalStatus.HasValue)
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
            catch (Exception ex)
            {
                throw new Exception("Failed to fetch person data list from database", ex);
            }
        }

        public async Task<PersonData?> GetDataByIdAsync(int id)
        {
            try
            {
                return await _context.PersonDatas.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to fetch person data from database", ex);
            }
        }

        public async Task UpdateDataAsync(PersonData entity)
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update person data on database", ex);
            }
        }
    }
}
