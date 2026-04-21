using Aggregator.Enums;

namespace DTO
{
    public class PersonDataFilterDto
    {
        public string? Name { get; set; }
        public Gender? Gender { get; set; }
        public MaritalStatus? MaritalStatus { get; set; }
        public bool? IsGraduated { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
