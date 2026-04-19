using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FirstProject.Models
{
    public class PersonData
    {
        public int Id { get; set; }

        [Required]
        [Length(2, 100)]
        public string Name { get; set; }

        [Required]
        public DateOnly DateOfBirth { get; set; }

        [Required]
        [Precision(3, 2)]
        public decimal HeightInFeet { get; set; }

        [Required]
        [Precision(5, 2)]
        public decimal WeightInKg { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string MaritalStatus { get; set; }

        [Required]
        public bool IsGraduated { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
