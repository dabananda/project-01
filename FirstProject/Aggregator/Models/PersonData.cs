using Aggregator.Enums;

namespace FirstProject.Models
{
    public class PersonData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public decimal HeightInFeet { get; set; }
        public decimal WeightInKg { get; set; }
        public Gender Gender { get; set; }
        public MaritalStatus MaritalStatus { get; set; }
        public bool IsGraduated { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
