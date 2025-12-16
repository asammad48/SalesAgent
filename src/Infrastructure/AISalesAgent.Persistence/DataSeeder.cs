using AISalesAgent.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AISalesAgent.Infrastructure.Persistence
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(SalesAgentDbContext context)
        {
            if (!context.AgentSettings.Any())
            {
                await context.AgentSettings.AddAsync(new AgentSettings
                {
                    AgentName = "Sales Agent",
                    AgentPersona = "You are a friendly and professional sales agent.",
                    DefaultTone = "Professional",
                    DefaultLanguage = "en-US",
                    TimeZone = "UTC"
                });
            }

            if (!context.Services.Any())
            {
                var services = new List<Service>
                {
                    new Service { ServiceName = "Lead Generation", ServiceDescription = "We help you find new customers." },
                    new Service { ServiceName = "Call Center", ServiceDescription = "We provide inbound and outbound call center services." },
                    new Service { ServiceName = "SEO", ServiceDescription = "We improve your search engine rankings." },
                    new Service { ServiceName = "Software/Web/App", ServiceDescription = "We build custom software, websites, and mobile apps." }
                };
                await context.Services.AddRangeAsync(services);

                if (!context.ServicePitches.Any())
                {
                    var leadGenService = services.FirstOrDefault(s => s.ServiceName == "Lead Generation");
                    var callCenterService = services.FirstOrDefault(s => s.ServiceName == "Call Center");
                    var seoService = services.FirstOrDefault(s => s.ServiceName == "SEO");
                    var softwareService = services.FirstOrDefault(s => s.ServiceName == "Software/Web/App");

                    var pitches = new List<ServicePitch>
                    {
                        new ServicePitch { Service = leadGenService, PitchType = "Short", PitchTitle = "Short Lead Gen Pitch", PitchText = "We can help you find more customers." },
                        new ServicePitch { Service = leadGenService, PitchType = "Detailed", PitchTitle = "Detailed Lead Gen Pitch", PitchText = "Our lead generation service is the best in the business." },
                        new ServicePitch { Service = callCenterService, PitchType = "Short", PitchTitle = "Short Call Center Pitch", PitchText = "We can handle your calls." },
                        new ServicePitch { Service = seoService, PitchType = "Short", PitchTitle = "Short SEO Pitch", PitchText = "We can get you to the top of Google." },
                        new ServicePitch { Service = softwareService, PitchType = "Short", PitchTitle = "Short Software Pitch", PitchText = "We can build your app." }
                    };
                    await context.ServicePitches.AddRangeAsync(pitches);
                }
            }

            if (!context.CTAs.Any())
            {
                var ctas = new List<CTA>
                {
                    new CTA { CTAText = "Book a call", CTAType = "ScheduleMeeting" },
                    new CTA { CTAText = "Visit our website", CTAType = "RequestInfo" },
                    new CTA { CTAText = "Can I send more details?", CTAType = "GeneralInquiry" }
                };
                await context.CTAs.AddRangeAsync(ctas);
            }

            await context.SaveChangesAsync();
        }
    }
}
