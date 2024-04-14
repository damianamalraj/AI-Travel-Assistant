using API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class OpenAiController : ControllerBase
  {
    private readonly IOpenAiRepository _openAiService;

    public OpenAiController(IOpenAiRepository openAiService)
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
    public async Task<IActionResult> AskAiAssistant(string request)
    {
      var result = await _openAiService.AskAiAssistant(request);

      return Ok(result);
    }
  }
}