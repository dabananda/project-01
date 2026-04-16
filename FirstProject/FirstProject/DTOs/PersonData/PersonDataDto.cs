namespace FirstProject.DTOs.Data
{
    public class PersonDataDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public decimal HeightInFeet { get; set; }
        public decimal WeightInKg { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public bool IsGraduated { get; set; }
    }
}
