using AISalesAgent.Application.DTOs;

namespace AISalesAgent.Application.Interfaces;

public interface IAIControlService
{
    Task ApproveResponseAsync(ApproveResponseDto approveResponseDto);
    Task RejectResponseAsync(RejectResponseDto rejectResponseDto);
    Task EscalateAsync(EscalateDto escalateDto);
    Task TakeoverAsync(TakeoverDto takeoverDto);
}
