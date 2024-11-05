using API.Model;

namespace API.Repository
{

  public class UserRepository : IUserRepository
  {
    public void UpdateUserPreferences(User currentUser, UserDto userDto)
    {
      if (userDto.Accommodations != null)
      {
        currentUser.Accommodations = currentUser.Accommodations ?? new List<Accommodation>();
        currentUser?.Accommodations?.Clear();
        foreach (var accommodationDto in userDto.Accommodations)
        {
          currentUser?.Accommodations?.Add(new Accommodation
          {
            Label = accommodationDto.Label,
            Selected = accommodationDto.Selected
          });
        }
      }

      if (userDto.Budgets != null)
      {
        currentUser.Budgets = currentUser.Budgets ?? new List<Budget>();
        currentUser?.Budgets?.Clear();
        foreach (var budgetDto in userDto.Budgets)
        {
          currentUser?.Budgets?.Add(new Budget
          {
            Label = budgetDto.Label,
            Amount = budgetDto.Amount
          });
        }
      }

      if (userDto.Diets != null)
      {
        currentUser.Diets = currentUser.Diets ?? new List<Diet>();
        currentUser?.Diets?.Clear();
        foreach (var dietDto in userDto.Diets)
        {
          currentUser?.Diets?.Add(new Diet
          {
            Label = dietDto.Label,
            Selected = dietDto.Selected
          });
        }
      }

      if (userDto.Foods != null)
      {
        currentUser.Foods = currentUser.Foods ?? new List<Food>();
        currentUser?.Foods?.Clear();
        foreach (var foodDto in userDto.Foods)
        {
          currentUser?.Foods?.Add(new Food
          {
            Label = foodDto.Label,
            //Vart tvungen att lägga till raden under
            Selected = foodDto.Selected
          });
        }
      }

      if (userDto.Transportations != null)
      {
        currentUser.Transportations = currentUser.Transportations ?? new List<Transportation>();
        currentUser?.Transportations?.Clear();
        foreach (var transportationDto in userDto.Transportations)
        {
          currentUser?.Transportations?.Add(new Transportation
          {

            Label = transportationDto.Label,
            Selected = transportationDto.Selected,

          });
        }
      }

      if (userDto.Vacations != null)
      {
        currentUser.Vacations = currentUser.Vacations ?? new List<Vacation>();
        currentUser?.Vacations?.Clear();
        foreach (var vacationDto in userDto.Vacations)
        {
          currentUser?.Vacations?.Add(new Vacation
          {
            Label = vacationDto.Label,
            Selected = vacationDto.Selected
          });
        }
      }
    }
  }
}
