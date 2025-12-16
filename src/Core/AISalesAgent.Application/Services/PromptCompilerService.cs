using AISalesAgent.Application.DTOs;
using AISalesAgent.Application.Interfaces;
using AISalesAgent.Application.Models;
using System.Text;
using System.Threading.Tasks;

namespace AISalesAgent.Application.Services
{
    public class PromptCompilerService : IPromptCompilerService
    {
        public Task<PromptCompilationResult> CompilePromptAsync(PromptContextDto context)
        {
            // This service is responsible for assembling the final prompt dynamically based on the provided context.
            // This ensures that the AI can only use approved pitches, CTAs, and services, and that all necessary
            // rules and constraints are injected into the prompt.

            var promptBuilder = new StringBuilder();

            // Start with the agent's persona.
            if (context.AgentSettings != null)
            {
                promptBuilder.AppendLine("## Agent Persona ##");
                promptBuilder.AppendLine(context.AgentSettings.AgentPersona);
            }

            // Add information about the lead.
            if (context.Lead != null)
            {
                promptBuilder.AppendLine("## Lead Information ##");
                promptBuilder.AppendLine($"- Name: {context.Lead.FirstName} {context.Lead.LastName}");
                promptBuilder.AppendLine($"- Company: {context.Lead.CompanyName}");
                promptBuilder.AppendLine($"- Sales Stage: {context.Lead.SalesStage}");
            }

            // Add information about the service and pitch.
            if (context.Service != null)
            {
                promptBuilder.AppendLine("## Service Information ##");
                promptBuilder.AppendLine($"- Service: {context.Service.ServiceName}");
                promptBuilder.AppendLine($"- Description: {context.Service.ServiceDescription}");
            }

            if (context.ServicePitch != null)
            {
                promptBuilder.AppendLine("## Service Pitch ##");
                promptBuilder.AppendLine(context.ServicePitch.PitchText);
            }

            // Add the call to action.
            if (context.CTA != null)
            {
                promptBuilder.AppendLine("## Call to Action ##");
                promptBuilder.AppendLine(context.CTA.CTAText);
            }

            // Add the objection response, if applicable.
            if (context.Objection != null)
            {
                promptBuilder.AppendLine("## Objection Response ##");
                promptBuilder.AppendLine(context.Objection.ResponseText);
            }

            // Add general rules and constraints.
            promptBuilder.AppendLine("## Rules ##");
            promptBuilder.AppendLine("- Do not negotiate price.");
            promptBuilder.AppendLine("- Escalate deals over $10,000.");
            promptBuilder.AppendLine("- Do not promise timelines.");

            var result = new PromptCompilationResult
            {
                FinalPrompt = promptBuilder.ToString(),
                Metadata = new PromptMetadata
                {
                    SalesStage = context.SalesStage,
                    Intent = "Sales",
                    IsEscalationRisk = false // This would be determined by more complex logic in a real implementation.
                }
            };

            return Task.FromResult(result);
        }
    }
}
