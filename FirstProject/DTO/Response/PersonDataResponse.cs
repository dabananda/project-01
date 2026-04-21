namespace DTO.Response
{
    public class PersonDataResponse
    {
        public int PersonId { get; set; }
        public string PersonName { get; set; }
        public DateOnly PersonDoB { get; set; }
        public decimal PersonHeight { get; set; }
        public decimal PersonWeight { get; set; }
        public string PersonGender { get; set; }
        public string PersonMaritalStatus { get; set; }
        public bool PersonIsGraduated { get; set; }
    }
}
