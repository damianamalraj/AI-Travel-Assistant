using API.Configurations;
using Microsoft.Extensions.Options;

namespace API.Services
{
  public class OpenAiService : IOpenAiService
  {
    private readonly OpenAiConfig _openAiConfig;

    public OpenAiService(IOptionsMonitor<OpenAiConfig> optionsMonitor)
    {
      _openAiConfig = optionsMonitor.CurrentValue;
    }

    public async Task<string> CreateAdvanceCompletion(string request)
    {
      var api = new OpenAI_API.OpenAIAPI(_openAiConfig.ApiKey);
      var result = await api.Completions.CreateCompletionAsync(new OpenAI_API.Completions.CompletionRequest(request, model: "davinci-002", max_tokens: 50, temperature: 0.1));

      return result.Completions[0].Text;
    }

    public async Task<object> AskAiAssistant(ReqDto request)
    {
      var api = new OpenAI_API.OpenAIAPI(_openAiConfig.ApiKey);
      var chat = api.Chat.CreateConversation();
      chat.Model = "gpt-4";
      chat.RequestParameters.MaxTokens = request.Token;
      chat.RequestParameters.Temperature = 0.1;

      chat.AppendSystemMessage(request.Prompt ?? "As an AI travel assistant, your role is to respond to travel-related inquiries such as destinations, flights, budgeting, accommodations, and attractions. When a user asks a travel question, like 'What are the best budget hotels in Paris?' or 'Can you suggest activities for kids in Tokyo?', provide concise and accurate responses. If a question is unrelated to travel, reply with: 'Sorry, I can't help you with that specific question. I'm here to assist with travel-related inquiries only.' When greeted, respond briefly: 'Hi, I'm your AI travel assistant. Shoot me your question.' This keeps the conversation focused and efficient, ensuring that responses are directly related to travel planning and are very concise and short.");

      chat.AppendUserInput(request.Question);

      var response = await chat.GetResponseFromChatbotAsync();
      Console.WriteLine(chat.MostRecentApiResult.Usage.PromptTokens);

      return new
      {
        response = response,
        tokens = chat.MostRecentApiResult.Usage.PromptTokens
      };
    }
  }
}
