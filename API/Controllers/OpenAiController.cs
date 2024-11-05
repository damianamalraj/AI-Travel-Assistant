using API.Repository;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  [Authorize]
  public class OpenAiController : ControllerBase
  {
    private readonly IOpenAiService _openAiService;

    public OpenAiController(IOpenAiService openAiService)
    {
      _openAiService = openAiService;
    }

    [HttpPost]
    [Route("CreateAdvanceCompletion")]
    public async Task<IActionResult> CreateAdvanceCompletion(string request)
    {
      var result = await _openAiService.CreateAdvanceCompletion(request);

      return Ok(result);
    }

    [HttpPost]
    [Route("AskAiAssistant")]
    public async Task<IActionResult> AskAiAssistant([FromBody] ReqDto request)
    {
      var result = await _openAiService.AskAiAssistant(request);

      return Ok(result);
    }

  }
}