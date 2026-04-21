using DTO;
using DTO.Request;
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

        public async Task<List<PersonData>> GetAllDataAsync(PersonDataFilterDto dto)
        {
            try
            {
                var datas = _context.PersonDatas.AsNoTracking().Where(x => x.IsDeleted == false).OrderByDescending(x => x.Id).AsQueryable();

                if (!string.IsNullOrEmpty(dto.Name))
                {
                    datas = datas.Where(x => x.Name.Contains(dto.Name));
                }

                if (!string.IsNullOrEmpty(dto.Gender))
                {
                    datas = datas.Where(x => x.Gender == dto.Gender);
                }

                if (!string.IsNullOrEmpty(dto.MaritalStatus))
                {
                    datas = datas.Where(x => x.MaritalStatus == dto.MaritalStatus);
                }

                if (dto.IsGraduated.HasValue)
                {
                    datas = datas.Where(x => x.IsGraduated == dto.IsGraduated.Value);
                }

                var skipRes = (dto.PageNumber - 1) * dto.PageSize;
                return await datas.Skip(skipRes).Take(dto.PageSize).ToListAsync();
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
