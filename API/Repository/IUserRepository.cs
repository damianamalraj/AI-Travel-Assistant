using API.Model;

namespace API.Repository
{
  public interface IUserRepository
  {
    void UpdateUserPreferences(User currentUser, UserDto userDto);
  }
}