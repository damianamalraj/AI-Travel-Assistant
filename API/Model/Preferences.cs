public class Preferences
{
  public int Id { get; set; }
  public string Label { get; set; }
  public bool Selected { get; set; }
}

public class Accommodation : Preferences
{

}

public class Diet : Preferences
{

}

public class Food : Preferences
{

}

public class Transportation : Preferences
{

}

public class Vacation : Preferences
{

}

public class Budget
{
  public int Id { get; set; }
  public string Label { get; set; }
  public int Amount { get; set; }
}

