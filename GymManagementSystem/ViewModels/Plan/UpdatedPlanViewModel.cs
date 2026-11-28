using System.ComponentModel.DataAnnotations;

namespace GymManagementSystem.ViewModels.Plan
{
    public class UpdatedPlanViewModel
    {
        [Display(Name = "Plan")]
        public string PlanName { get; set; } = default!;
        [Required(ErrorMessage = "Description is Required!")]
        [StringLength(200,MinimumLength = 5, ErrorMessage = "Description must be between 5 and 200!")]
        public string Description { get; set; } = default!;
        [Required(ErrorMessage = "Duration Days is Required!")]
        [Display(Name = "Duration(Days)")]
        [Range(1,365, ErrorMessage = "Duration must be between 1 and 365!")]
        public int DurationDays { get; set; }
        [Required(ErrorMessage = "Price is Required!")]
        [Display(Name = "Price(EGP)")]
        [Range(0.1, 10000, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }
    }
}
