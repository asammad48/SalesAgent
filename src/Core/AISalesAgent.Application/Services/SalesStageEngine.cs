using AISalesAgent.Application.DTOs;
using AISalesAgent.Application.Interfaces;
using AISalesAgent.Domain.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AISalesAgent.Application.Services
{
    public class SalesStageEngine : ISalesStageEngine
    {
        private readonly ILeadRepository _leadRepository;
        private readonly IUnitOfWork _unitOfWork;

        // Defines the valid transitions between sales stages.
        private readonly Dictionary<SalesStage, HashSet<SalesStage>> _validTransitions = new()
        {
            { SalesStage.NEW, new HashSet<SalesStage> { SalesStage.DISCOVERY, SalesStage.LOST, SalesStage.ESCALATED } },
            { SalesStage.DISCOVERY, new HashSet<SalesStage> { SalesStage.QUALIFIED, SalesStage.LOST, SalesStage.ESCALATED } },
            { SalesStage.QUALIFIED, new HashSet<SalesStage> { SalesStage.PITCH_SENT, SalesStage.LOST, SalesStage.ESCALATED } },
            { SalesStage.PITCH_SENT, new HashSet<SalesStage> { SalesStage.OBJECTION_HANDLING, SalesStage.FOLLOW_UP, SalesStage.NEGOTIATION, SalesStage.WON, SalesStage.LOST, SalesStage.ESCALATED } },
            { SalesStage.OBJECTION_HANDLING, new HashSet<SalesStage> { SalesStage.PITCH_SENT, SalesStage.FOLLOW_UP, SalesStage.NEGOTIATION, SalesStage.WON, SalesStage.LOST, SalesStage.ESCALATED } },
            { SalesStage.FOLLOW_UP, new HashSet<SalesStage> { SalesStage.PITCH_SENT, SalesStage.NEGOTIATION, SalesStage.WON, SalesStage.LOST, SalesStage.ESCALATED } },
            { SalesStage.NEGOTIATION, new HashSet<SalesStage> { SalesStage.WON, SalesStage.LOST, SalesStage.ESCALATED } }
        };

        public SalesStageEngine(ILeadRepository leadRepository, IUnitOfWork unitOfWork)
        {
            _leadRepository = leadRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> TransitionToStageAsync(StageTransitionRequest request)
        {
            if (request.Lead == null)
            {
                return false;
            }

            var currentStage = request.Lead.SalesStage;

            // Terminal states cannot be transitioned from.
            if (!_validTransitions.ContainsKey(currentStage))
            {
                return false;
            }

            // Check if the requested transition is valid.
            if (!_validTransitions[currentStage].Contains(request.NewStage))
            {
                return false;
            }

            // Update the lead's sales stage.
            request.Lead.SalesStage = request.NewStage;

            _leadRepository.Update(request.Lead);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
