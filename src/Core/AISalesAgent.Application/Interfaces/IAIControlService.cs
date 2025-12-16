using AISalesAgent.Application.DTOs;
using System.Threading.Tasks;

namespace AISalesAgent.Application.Interfaces;

public interface IAIControlService
{
    Task ApproveResponseAsync(ApproveResponseDto approveResponseDto);
    Task RejectResponseAsync(RejectResponseDto rejectResponseDto);
    Task EscalateAsync(EscalateDto escalateDto);
    Task TakeoverAsync(TakeoverDto takeoverDto);
}
