namespace DTO.Response
{
    public class PersonDataResponse
    {
        public int PersonId { get; set; }
        public string PersonName { get; set; }
        public DateOnly PersonDateOfBirth { get; set; }
        public decimal PersonHeightInFeet { get; set; }
        public decimal PersonWeightInKg { get; set; }
        public string PersonGender { get; set; }
        public string PersonMaritalStatus { get; set; }
        public bool PersonIsGraduated { get; set; }
    }
}
