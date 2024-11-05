using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Model
{

  public class User : IdentityUser
  {
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
    public string? Gender { get; set; }

    public virtual ICollection<Accommodation>? Accommodations { get; set; }
    public virtual ICollection<Budget>? Budgets { get; set; }
    public virtual ICollection<Diet>? Diets { get; set; }
    public virtual ICollection<Food>? Foods { get; set; }
    public virtual ICollection<Transportation>? Transportations { get; set; }
    public virtual ICollection<Vacation>? Vacations { get; set; }

    public User()
    {
      Accommodations = new List<Accommodation>();
      Budgets = new List<Budget>();
      Diets = new List<Diet>();
      Foods = new List<Food>();
      Transportations = new List<Transportation>();
      Vacations = new List<Vacation>();
    }
  }

}