using AISalesAgent.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AISalesAgent.Infrastructure.Persistence
{
    public class SalesAgentDbContext : DbContext
    {
        public SalesAgentDbContext(DbContextOptions<SalesAgentDbContext> options) : base(options)
        {
        }

        public DbSet<AIResponse> AIResponses { get; set; }
        public DbSet<AgentSettings> AgentSettings { get; set; }
        public DbSet<CTA> CTAs { get; set; }
        public DbSet<ConversationQuestion> ConversationQuestions { get; set; }
        public DbSet<EscalationRule> EscalationRules { get; set; }
        public DbSet<Lead> Leads { get; set; }
        public DbSet<Objection> Objections { get; set; }
        public DbSet<SalesTask> SalesTasks { get; set; }
        public DbSet<SalesTimeline> SalesTimelines { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServicePitch> ServicePitches { get; set; }
        public DbSet<ExecutionLog> ExecutionLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AgentSettings>(entity =>
            {
                entity.HasKey(e => e.AgentId);
                entity.Property(e => e.AgentId).HasDefaultValueSql("NEWID()");
                entity.Property(e => e.AgentName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.AgentPersona).IsRequired();
                entity.Property(e => e.DefaultTone).IsRequired().HasMaxLength(50);
                entity.Property(e => e.DefaultLanguage).IsRequired().HasMaxLength(20).HasDefaultValue("en-US");
                entity.Property(e => e.TimeZone).IsRequired().HasMaxLength(100);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("GETUTCDATE()");
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.HasKey(e => e.ServiceId);
                entity.Property(e => e.ServiceId).HasDefaultValueSql("NEWID()");
                entity.Property(e => e.ServiceName).IsRequired().HasMaxLength(200);
                entity.Property(e => e.ServiceDescription).IsRequired();
                entity.Property(e => e.IsActive).IsRequired().HasDefaultValue(true);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("GETUTCDATE()");

                entity.HasMany(s => s.Pitches)
                    .WithOne(p => p.Service)
                    .HasForeignKey(p => p.ServiceId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ServicePitch>(entity =>
            {
                entity.HasKey(e => e.PitchId);
                entity.Property(e => e.PitchId).HasDefaultValueSql("NEWID()");
                entity.Property(e => e.PitchType).IsRequired().HasMaxLength(50);
                entity.Property(e => e.PitchTitle).IsRequired().HasMaxLength(200);
                entity.Property(e => e.PitchText).IsRequired();
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("GETUTCDATE()");
            });

            modelBuilder.Entity<CTA>(entity =>
            {
                entity.HasKey(e => e.CTAId);
                entity.Property(e => e.CTAId).HasDefaultValueSql("NEWID()");
                entity.Property(e => e.CTAText).IsRequired().HasMaxLength(500);
                entity.Property(e => e.CTAType).HasMaxLength(50);
                entity.Property(e => e.IsActive).IsRequired().HasDefaultValue(true);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            });

            modelBuilder.Entity<ConversationQuestion>(entity =>
            {
                entity.HasKey(e => e.QuestionId);
                entity.Property(e => e.QuestionId).HasDefaultValueSql("NEWID()");
                entity.Property(e => e.QuestionText).IsRequired();
                entity.Property(e => e.QuestionType).HasMaxLength(50);
                entity.Property(e => e.IsActive).IsRequired().HasDefaultValue(true);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            });

            modelBuilder.Entity<Lead>(entity =>
            {
                entity.HasKey(e => e.LeadId);
                entity.Property(e => e.LeadId).HasDefaultValueSql("NEWID()");
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.PhoneNumber).IsUnique();
                entity.Property(e => e.Status).IsRequired().HasMaxLength(50);
                entity.Property(e => e.SalesStage)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasConversion<string>();
                entity.Property(e => e.LeadScore).HasDefaultValue(0);
                entity.Property(e => e.AssignedTo).HasDefaultValue("AI");
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("GETUTCDATE()");
            });

            modelBuilder.Entity<SalesTask>(entity =>
            {
                entity.HasKey(e => e.TaskId);
                entity.Property(e => e.TaskId).HasDefaultValueSql("NEWID()");
                entity.Property(e => e.TaskType).IsRequired().HasMaxLength(100);
                entity.Property(e => e.TaskState)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasConversion<string>();
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

                entity.HasOne(st => st.Lead)
                    .WithMany(l => l.SalesTasks)
                    .HasForeignKey(st => st.LeadId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(st => st.ExecutionLogs)
                    .WithOne(el => el.SalesTask)
                    .HasForeignKey(el => el.SalesTaskId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<SalesTimeline>(entity =>
            {
                entity.HasKey(e => e.EventId);
                entity.Property(e => e.EventId).HasDefaultValueSql("NEWID()");
                entity.Property(e => e.EventType).IsRequired().HasMaxLength(100);
                entity.Property(e => e.SourceChannel).HasMaxLength(50);
                entity.Property(e => e.Actor).HasMaxLength(100);
                entity.Property(e => e.EventTimestamp).HasDefaultValueSql("GETUTCDATE()");

                entity.HasOne(st => st.Lead)
                    .WithMany(l => l.TimelineEvents)
                    .HasForeignKey(st => st.LeadId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(st => st.AIResponse)
                    .WithOne(ar => ar.TimelineEvent)
                    .HasForeignKey<AIResponse>(ar => ar.TimelineEventId);
            });

            modelBuilder.Entity<AIResponse>(entity =>
            {
                entity.HasKey(e => e.ResponseId);
                entity.Property(e => e.ResponseId).HasDefaultValueSql("NEWID()");
                entity.Property(e => e.CompiledPrompt).IsRequired();
                entity.Property(e => e.ResponseText).IsRequired();
                entity.Property(e => e.ModelUsed).HasMaxLength(100);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            });

            modelBuilder.Entity<EscalationRule>(entity =>
            {
                entity.HasKey(e => e.RuleId);
                entity.Property(e => e.RuleId).HasDefaultValueSql("NEWID()");
                entity.Property(e => e.RuleName).IsRequired().HasMaxLength(200);
                entity.Property(e => e.ConditionType).IsRequired().HasMaxLength(100);
                entity.Property(e => e.ConditionValue).IsRequired().HasMaxLength(500);
                entity.Property(e => e.IsActive).IsRequired().HasDefaultValue(true);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            });

            modelBuilder.Entity<Objection>(entity =>
            {
                entity.HasKey(e => e.ObjectionId);
                entity.Property(e => e.ObjectionId).HasDefaultValueSql("NEWID()");
                entity.Property(e => e.ObjectionName).IsRequired().HasMaxLength(200);
                entity.Property(e => e.ResponseText).IsRequired();
                entity.Property(e => e.IsActive).IsRequired().HasDefaultValue(true);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("GETUTCDATE()");
            });

            modelBuilder.Entity<ExecutionLog>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasDefaultValueSql("NEWID()");
                entity.Property(e => e.Timestamp).HasDefaultValueSql("GETUTCDATE()");
            });
        }
    }
}
