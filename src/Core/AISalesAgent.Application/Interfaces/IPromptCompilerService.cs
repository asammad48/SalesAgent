using AISalesAgent.Application.DTOs;
using AISalesAgent.Application.Models;
using System.Threading.Tasks;

namespace AISalesAgent.Application.Interfaces
{
    public interface IPromptCompilerService
    {
        Task<PromptCompilationResult> CompilePromptAsync(PromptContextDto context);
    }
}
