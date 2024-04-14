namespace API.Repository
{
  public interface IOpenAiRepository
  {
    Task<string> CreateAdvanceCompletion(string request);
    Task<object> AskAiAssistant(string request);
  }
}