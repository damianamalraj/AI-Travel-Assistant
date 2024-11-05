namespace API.Services
{
  public interface IOpenAiService
  {
    Task<string> CreateAdvanceCompletion(string request);
    Task<object> AskAiAssistant(ReqDto request);

  }
}