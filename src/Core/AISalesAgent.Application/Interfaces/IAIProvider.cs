using AISalesAgent.Domain.Models;
using System.Threading.Tasks;

namespace AISalesAgent.Application.Interfaces
{
    public interface IAIProvider
    {
        Task<StructuredAIResponse> SendPromptAsync(string prompt, object context);
    }
}
