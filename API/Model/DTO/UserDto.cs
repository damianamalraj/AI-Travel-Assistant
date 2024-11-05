using System.ComponentModel.DataAnnotations;

namespace API.Model
{

  public class UserDto
  {
    [Required(ErrorMessage = "First name is required")]
    public string? FirstName { get; set; }
    [Required(ErrorMessage = "Last name is required")]
    public string? LastName { get; set; }
    [Required(ErrorMessage = "City is required")]
    public string? City { get; set; }
    [Required(ErrorMessage = "Country is required")]
    public string? Country { get; set; }

    public string? UserName { get; set; }
    public string? Gender { get; set; }

    public ICollection<AccommodationDto>? Accommodations { get; set; }
    public ICollection<BudgetDto>? Budgets { get; set; }
    public ICollection<DietDto>? Diets { get; set; }
    public ICollection<FoodDto>? Foods { get; set; }
    public ICollection<TransportationDto>? Transportations { get; set; }
    public ICollection<VacationDto>? Vacations { get; set; }
  }
}