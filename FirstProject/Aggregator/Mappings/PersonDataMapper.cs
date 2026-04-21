using DTO.Request;
using DTO.Response;
using FirstProject.Models;

namespace Aggregator.Mappings
{
    public static class PersonDataMapper
    {
        public static PersonDataResponse ToDto(this PersonData data)
        {
            if (data == null) return null;
            return new PersonDataResponse
            {
                PersonId = data.Id,
                PersonName = data.Name,
                PersonDoB = data.DateOfBirth,
                PersonHeight = data.HeightInFeet,
                PersonWeight = data.WeightInKg,
                PersonGender = data.Gender,
                PersonMaritalStatus = data.MaritalStatus,
                PersonIsGraduated = data.IsGraduated
            };
        }

        public static List<PersonDataResponse> ToDtoList(this List<PersonData> dataList)
        {
            if (dataList == null) return null;
            return dataList.Select(data => data.ToDto()).ToList();
        }

        public static PersonData ToEntity(this CreatePersonDataRequest response)
        {
            if (response == null) return null;
            return new PersonData
            {
                Name = response.PersonName,
                DateOfBirth = response.PersonDoB,
                HeightInFeet = response.PersonHeight,
                WeightInKg = response.PersonWeight,
                Gender = response.PersonGender,
                MaritalStatus = response.PersonMaritalStatus,
                IsGraduated = response.PersonIsGraduated
            };
        }
    }
}
