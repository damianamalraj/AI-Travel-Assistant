using API.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : IdentityDbContext<User>
{

  public DbSet<Accommodation> Accommodation { get; set; }
  public DbSet<Budget> Budget { get; set; }
  public DbSet<Diet> Diet { get; set; }
  public DbSet<Food> Food { get; set; }
  public DbSet<Transportation> Transportation { get; set; }
  public DbSet<Vacation> Vacation { get; set; }

  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
      base(options)
  { }

}