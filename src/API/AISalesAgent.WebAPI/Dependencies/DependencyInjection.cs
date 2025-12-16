using AISalesAgent.Application.Interfaces;
using AISalesAgent.Infrastructure.Persistence;
using AISalesAgent.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AISalesAgent.WebAPI.Dependencies
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SalesAgentDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    builder => builder.MigrationsAssembly(typeof(SalesAgentDbContext).Assembly.FullName)));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IAIResponseRepository, AIResponseRepository>();
            services.AddScoped<IAgentSettingsRepository, AgentSettingsRepository>();
            services.AddScoped<ICTARepository, CTARepository>();
            services.AddScoped<IConversationQuestionRepository, ConversationQuestionRepository>();
            services.AddScoped<IEscalationRuleRepository, EscalationRuleRepository>();
            services.AddScoped<ILeadRepository, LeadRepository>();
            services.AddScoped<IObjectionRepository, ObjectionRepository>();
            services.AddScoped<ISalesTaskRepository, SalesTaskRepository>();
            services.AddScoped<ISalesTimelineRepository, SalesTimelineRepository>();
            services.AddScoped<IServicePitchRepository, ServicePitchRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();

            return services;
        }
    }
}
