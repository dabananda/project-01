using Aggregator.Enums;
using Dapper;
using FirstProject.Models;
using FirstProject.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Text;

namespace FirstProject.Repositories
{
    public class DataRepo : IDataRepo
    {
        private readonly string _connectionString;

        public DataRepo(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection") ?? throw new ArgumentNullException("Db Connection failed");
        }

        private IDbConnection CreateConnection() => new SqlConnection(_connectionString);

        public async Task<PersonData> CreateDataAsync(PersonData data)
        {
            try
            {
                using var connection = CreateConnection();
                var query = "INSERT INTO PersonDatas (Name, DateOfBirth, HeightInFeet, WeightInKg, Gender, MaritalStatus, IsGraduated, IsDeleted) " +
                            "VALUES (@Name, @DateOfBirth, @HeightInFeet, @WeightInKg, @Gender, @MaritalStatus, @IsGraduated, 0); " +
                            "SELECT CAST(SCOPE_IDENTITY() as int)";
                var id = await connection.QuerySingleAsync<int>(query, data);
                data.Id = id;
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to create person data on database", ex);
            }
        }

        public async Task DeleteDataAsync(PersonData data)
        {
            try
            {
                using var connection = CreateConnection();
                var query = "UPDATE PersonDatas SET IsDeleted = 1 WHERE Id = @Id";
                await connection.ExecuteAsync(query, new { data.Id });
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
                using var connection = CreateConnection();
                var sql = new StringBuilder("SELECT * FROM PersonDatas WHERE IsDeleted = 0");
                
                var parameters = new DynamicParameters();
                if (!string.IsNullOrEmpty(name))
                {
                    sql.Append(" AND Name LIKE @Name");
                    parameters.Add("Name", $"%{name}%");
                }

                if (gender.HasValue)
                {
                    sql.Append(" AND Gender = @Gender");
                    parameters.Add("Gender", gender.Value.ToString());
                }

                if (maritalStatus.HasValue)
                {
                    sql.Append(" AND MaritalStatus = @MaritalStatus");
                    parameters.Add("MaritalStatus", maritalStatus.Value.ToString());
                }

                if (isGraduated.HasValue)
                {
                    sql.Append(" AND IsGraduated = @IsGraduated");
                    parameters.Add("IsGraduated", isGraduated.Value);
                }

                var skip = (pageNumber - 1) * pageSize;
                sql.Append(" ORDER BY Id DESC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY;");
                parameters.Add("Skip", skip);
                parameters.Add("Take", pageSize);

                var result = await connection.QueryAsync<PersonData>(sql.ToString(), parameters);
                return result.ToList();
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
                using var connection = CreateConnection();
                var query = "SELECT * FROM PersonDatas WHERE Id = @Id AND IsDeleted = 0";
                return await connection.QuerySingleOrDefaultAsync<PersonData>(query, new { Id = id });
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
                using var connection = CreateConnection();
                var query = "UPDATE PersonDatas SET Name = @Name, DateOfBirth = @DateOfBirth, HeightInFeet = @HeightInFeet, WeightInKg = @WeightInKg, Gender = @Gender, MaritalStatus = @MaritalStatus, IsGraduated = @IsGraduated WHERE Id = @Id";
                await connection.ExecuteAsync(query, entity);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update person data on database", ex);
            }
        }
    }
}
