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
                PersonDateOfBirth = data.DateOfBirth,
                PersonHeightInFeet = data.HeightInFeet,
                PersonWeightInKg = data.WeightInKg,
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

        public static PersonData ToEntity(this PersonDataResponse response)
        {
            if (response == null) return null;
            return new PersonData
            {
                Id = response.PersonId,
                Name = response.PersonName,
                DateOfBirth = response.PersonDateOfBirth,
                HeightInFeet = response.PersonHeightInFeet,
                WeightInKg = response.PersonWeightInKg,
                Gender = response.PersonGender,
                MaritalStatus = response.PersonMaritalStatus,
                IsGraduated = response.PersonIsGraduated
            };
        }
    }
}
