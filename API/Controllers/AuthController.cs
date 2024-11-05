using System.Security.Claims;
using API.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using API.Repository;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AuthController : ControllerBase
{
  private readonly UserManager<User> _userManager;
  private readonly SignInManager<User> _signInManager;
  private readonly IUserRepository _userRepository;
  private readonly ApplicationDbContext _context;

  public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, IUserRepository userRepository, ApplicationDbContext context)
  {
    _userManager = userManager;
    _signInManager = signInManager;
    _userRepository = userRepository;
    _context = context;
  }

  [HttpGet("login-google")]
  [AllowAnonymous]
  public IActionResult LoginGoogle()
  {
    var properties = new AuthenticationProperties { RedirectUri = Url.Action("GoogleResponse") };

    return Challenge(properties, GoogleDefaults.AuthenticationScheme);
  }

  [HttpGet("google-response")]
  [AllowAnonymous]
  public async Task<IActionResult> GoogleResponse()
  {

    var result = await HttpContext.AuthenticateAsync(IdentityConstants.ApplicationScheme);

    if (!result.Succeeded)
      return BadRequest("Error authenticating with Google");

    var claims = result.Principal?.Identities.FirstOrDefault()?.Claims.Select(claim => new
    {
      claim.Issuer,
      claim.OriginalIssuer,
      claim.Type,
      claim.Value
    }).ToList();

    var emailClaim = result.Principal?.FindFirst(ClaimTypes.Email)?.Value;
    var user = await _userManager.FindByEmailAsync(emailClaim);
    if (user == null)
    {
      user = new User { UserName = emailClaim, Email = emailClaim };
      var createResult = await _userManager.CreateAsync(user);
      if (!createResult.Succeeded)
      {
        return BadRequest("Could not create user account");
      }
    }

    await _signInManager.SignInAsync(user, isPersistent: false);

    return Redirect("https://localhost:5173/");
  }

[HttpGet("login-facebook")]
    [AllowAnonymous]
    public IActionResult LoginFacebook()
    {
        var properties = new AuthenticationProperties { RedirectUri = Url.Action("FacebookResponse") };
        return Challenge(properties, FacebookDefaults.AuthenticationScheme);
    }

    [HttpGet("facebook-response")]
    [AllowAnonymous]
    public async Task<IActionResult> FacebookResponse()
    {
        var result = await HttpContext.AuthenticateAsync(IdentityConstants.ApplicationScheme);

        if (!result.Succeeded)
            return BadRequest("Error authenticating with Facebook");

        var claims = result.Principal?.Identities.FirstOrDefault()?.Claims.Select(claim => new
        {
            claim.Issuer,
            claim.OriginalIssuer,
            claim.Type,
            claim.Value
        }).ToList();

        var emailClaim = result.Principal?.FindFirst(ClaimTypes.Email)?.Value;
        var user = await _userManager.FindByEmailAsync(emailClaim);
        if (user == null)
        {
            user = new User { UserName = emailClaim, Email = emailClaim };
            var createResult = await _userManager.CreateAsync(user);
            if (!createResult.Succeeded)
            {
                return BadRequest("Could not create user account");
            }
        }
        await _signInManager.SignInAsync(user, isPersistent: false);

    return Redirect("https://localhost:5173/");
  }

  [HttpGet("user")]
  public async Task<IActionResult> GetUser()
  {
    var user = await _userManager.GetUserAsync(User);

    if (user == null)
    {
      return NotFound("User not found");
    }

    await _context.Entry(user).Collection(u => u.Accommodations).LoadAsync();
    await _context.Entry(user).Collection(u => u.Vacations).LoadAsync();
    await _context.Entry(user).Collection(u => u.Budgets).LoadAsync();
    await _context.Entry(user).Collection(u => u.Diets).LoadAsync();
    await _context.Entry(user).Collection(u => u.Foods).LoadAsync();
    await _context.Entry(user).Collection(u => u.Transportations).LoadAsync();

    return Ok(user);
  }

  [HttpPatch("user")]
  public async Task<IActionResult> UpdateUser([FromBody] UserDto userDto)
  {
    var currentUser = await _userManager.GetUserAsync(User);

    if (currentUser == null)
    {
      return BadRequest("Could not find user account");
    }

    currentUser.FirstName = userDto.FirstName;
    currentUser.LastName = userDto.LastName;
    currentUser.UserName = userDto.UserName;
    currentUser.City = userDto.City;
    currentUser.Country = userDto.Country;
    currentUser.Gender = userDto.Gender;

    await _context.Entry(currentUser).Collection(u => u.Budgets).LoadAsync();
    await _context.Entry(currentUser).Collection(u => u.Accommodations).LoadAsync();
    await _context.Entry(currentUser).Collection(u => u.Diets).LoadAsync();
    await _context.Entry(currentUser).Collection(u => u.Foods).LoadAsync();
    await _context.Entry(currentUser).Collection(u => u.Transportations).LoadAsync();
    await _context.Entry(currentUser).Collection(u => u.Vacations).LoadAsync();

    _userRepository.UpdateUserPreferences(currentUser, userDto);

    var result = await _userManager.UpdateAsync(currentUser);
    if (!result.Succeeded)
    {
      return BadRequest("Could not update user account");
    }

    await _context.SaveChangesAsync();

    return Ok(currentUser);
  }

  [HttpDelete("user")]
  public async Task<IActionResult> DeleteUser()
  {
    var user = await _userManager.GetUserAsync(User);

    if (user == null)
    {
      return NotFound("User not found");
    }

    _context.Entry(user).Collection(u => u.Accommodations).Load();
    _context.Entry(user).Collection(u => u.Vacations).Load();
    _context.Entry(user).Collection(u => u.Budgets).Load();
    _context.Entry(user).Collection(u => u.Diets).Load();
    _context.Entry(user).Collection(u => u.Foods).Load();
    _context.Entry(user).Collection(u => u.Transportations).Load();

    var result = await _userManager.DeleteAsync(user);

    if (!result.Succeeded)
    {
      return BadRequest("Could not delete user account");
    }
    return Ok();
  }

  [HttpGet("logout")]
  public async Task<IActionResult> Logout()
  {
    await _signInManager.SignOutAsync();
    return Ok();
  }

  [HttpPost("validate-password")]
  public async Task<IActionResult> ValidatePassword([FromBody] PasswordValidationDto passwordValidationDto)
  {
    var user = await _userManager.GetUserAsync(User);
    if (user == null)
    {
      return NotFound("User not found");
    }

    var passwordValid = await _userManager.CheckPasswordAsync(user, passwordValidationDto.Password);
    if (!passwordValid)
    {
      return Unauthorized("Invalid password");
    }

    return Ok(new { valid = true });
  }
}

