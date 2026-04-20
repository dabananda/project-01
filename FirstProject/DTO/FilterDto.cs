namespace DTO
{
    public class FilterDto
    {
        public string? Name { get; set; }
        public string? Gender { get; set; }
        public string? MaritalStatus { get; set; }
        public bool? IsGraduated { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
