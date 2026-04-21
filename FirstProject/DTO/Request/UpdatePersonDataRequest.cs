using System.ComponentModel.DataAnnotations;

namespace DTO.Request
{
    public class UpdatePersonDataRequest
    {
        [Required(ErrorMessage = "Name is required.")]
        [Length(2, 100, ErrorMessage = "Name must be between 2 to 100 characters.")]
        public string PersonName { get; set; }

        [Required(ErrorMessage = "Date of birth is required")]
        [DataType(DataType.Date)]
        public DateOnly PersonDoB { get; set; }
        [Required(ErrorMessage = "Height is required.")]
        [Range(1, 10, ErrorMessage = "Height must be from 1 feet to 10 feet")]
        public decimal PersonHeight { get; set; }

        [Required(ErrorMessage = "Weight is required.")]
        [Range(1, 500, ErrorMessage = "Weight must be from 1 feet to 500 kg")]
        public decimal PersonWeight { get; set; }
        [Required(ErrorMessage = "Gender is required.")]
        public string PersonGender { get; set; }

        [Required(ErrorMessage = "Marital Status is required.")]
        public string PersonMaritalStatus { get; set; }

        [Required(ErrorMessage = "Graduation Status is required.")]
        public bool PersonIsGraduated { get; set; }
    }
}
