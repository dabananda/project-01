using Aggregator.Enums;

namespace DTO.Request
{
    public class UpdatePersonDataRequest
    {
        public string PersonName { get; set; }
        public DateOnly PersonDoB { get; set; }
        public decimal PersonHeight { get; set; }
        public decimal PersonWeight { get; set; }
        public Gender PersonGender { get; set; }
        public MaritalStatus PersonMaritalStatus { get; set; }
        public bool PersonIsGraduated { get; set; }
    }
}
