public class PreferencesDto
{
  public int Id { get; set; }
  public string Label { get; set; }
  public bool Selected { get; set; }
}

public class AccommodationDto : PreferencesDto
{

}

public class DietDto : PreferencesDto
{

}

public class FoodDto : PreferencesDto
{

}

public class TransportationDto : PreferencesDto
{

}

public class VacationDto : PreferencesDto
{

}

public class BudgetDto
{
  public int Id { get; set; }
  public string Label { get; set; }
  public int Amount { get; set; }
}

