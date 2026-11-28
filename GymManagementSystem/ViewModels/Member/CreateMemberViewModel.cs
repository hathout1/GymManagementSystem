using GymManagementSystem.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace GymManagementSystem.ViewModels.Member
{
    public class CreateMemberViewModel
    {
        [Required(ErrorMessage = "Name is required!")]
        [StringLength(50,MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 character")]
        [RegularExpression(@"^[a-zA-Z\s]+$",ErrorMessage = "Name can only contain Alphabet")]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "Email is required!")]
        [StringLength(100, ErrorMessage = "Email must be less than 100 character!")]
        [EmailAddress(ErrorMessage = "Invalid email format!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^(010|011|012|015)\d{8}$", ErrorMessage = "Phone Number must be a valid egyption number")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; } = null!;
        [Required(ErrorMessage = "Date of Birth is required!")]
        [DataType(DataType.Date)]
        [Display(Name = "Date Of Birth")]
        public DateOnly DateOfBirth { get; set; }
        [Required(ErrorMessage = "Gender is required!")]
        [EnumDataType(typeof(Gender), ErrorMessage = "Invalid Gender!")]
        public Gender Gender { get; set; }
        [Required(ErrorMessage = "Building Number is required!")]
        [Range(1, 9000, ErrorMessage = "Building number must be between 1 and 9000")]
        [Display(Name = "Building Number")]
        public int BuildingNumber { get; set; }
        [Required(ErrorMessage = "City is required!")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "City must be between 2 and 100 character")]
        [RegularExpression(@"^[a-zA-z\s]+$", ErrorMessage = "City can only contain Alphabet")]
        public string City { get; set; } = default!;
        [Required(ErrorMessage = "Street is required")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "Street must be between 2 and 150 character!")]
        [RegularExpression(@"^[a-zA-z0-9\s]+$", ErrorMessage = "Street can only contain Alphabet and numbers!")]
        public string Street { get; set; } = default!;
    }
}
