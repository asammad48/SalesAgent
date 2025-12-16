
-- =================================================================================
-- Step 2: DATABASE SCHEMA
-- This script contains the full SQL schema for the AI Sales Agent system.
-- =================================================================================

-- ---------------------------------------------------------------------------------
-- Table: AgentSettings
-- Purpose: Stores the core configuration and persona of the AI agent.
-- Why: Centralizes all high-level settings, allowing the founder to define the
-- AI's behavior and personality from the UI without any code changes.
-- ---------------------------------------------------------------------------------
CREATE TABLE AgentSettings (
    AgentId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    AgentName NVARCHAR(100) NOT NULL,
    AgentPersona NVARCHAR(MAX) NOT NULL, -- Detailed description of the AI's personality (e.g., "friendly, professional, slightly informal")
    DefaultTone NVARCHAR(50) NOT NULL, -- e.g., "Professional", "Casual", "Enthusiastic"
    DefaultLanguage NVARCHAR(20) NOT NULL DEFAULT 'en-US',
    TimeZone NVARCHAR(100) NOT NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE()
);

-- ---------------------------------------------------------------------------------
-- Table: Services
-- Purpose: Defines the products or services the AI agent is selling.
-- Why: Allows the founder to dynamically add, remove, or modify services.
-- Each service is a distinct offering the AI can pitch to leads.
-- ---------------------------------------------------------------------------------
CREATE TABLE Services (
    ServiceId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    ServiceName NVARCHAR(200) NOT NULL,
    ServiceDescription NVARCHAR(MAX) NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE()
);

-- ---------------------------------------------------------------------------------
-- Table: ServicePitches
-- Purpose: Stores multiple, context-specific pitches for each service.
-- Why: This is critical for a dynamic system. Instead of a single, static pitch,
-- the AI can choose the most appropriate one based on the conversation stage
-- (e.g., a short opener, a detailed follow-up, an ROI-focused argument).
-- ---------------------------------------------------------------------------------
CREATE TABLE ServicePitches (
    PitchId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    ServiceId UNIQUEIDENTIFIER NOT NULL,
    PitchType NVARCHAR(50) NOT NULL, -- e.g., 'ShortIntro', 'DetailedExplanation', 'ROI', 'ObjectionResponse'
    PitchTitle NVARCHAR(200) NOT NULL, -- For easy identification in the UI
    PitchText NVARCHAR(MAX) NOT NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    FOREIGN KEY (ServiceId) REFERENCES Services(ServiceId) ON DELETE CASCADE
);

-- ---------------------------------------------------------------------------------
-- Table: CTAs (Call to Actions)
-- Purpose: A library of predefined calls to action.
-- Why: Enables the AI to select the best CTA based on the context, such as
-- "Book a call", "Visit our website", or "Can I send more details?". This avoids
-- repetitive, hardcoded phrases and allows for A/B testing from the UI.
-- ---------------------------------------------------------------------------------
CREATE TABLE CTAs (
    CTAId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    CTAText NVARCHAR(500) NOT NULL,
    CTAType NVARCHAR(50), -- e.g., 'ScheduleMeeting', 'RequestInfo', 'GeneralInquiry'
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE()
);

-- ---------------------------------------------------------------------------------
-- Table: ConversationQuestions
-- Purpose: A bank of questions for the AI to ask leads.
-- Why: To guide the conversation and qualify leads effectively. Questions can be
-- tagged by stage (e.g., 'Discovery', 'Qualification') and used by the AI to
-- gather necessary information before pitching.
-- ---------------------------------------------------------------------------------
CREATE TABLE ConversationQuestions (
    QuestionId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    QuestionText NVARCHAR(MAX) NOT NULL,
    QuestionType NVARCHAR(50), -- e.g., 'OpenEnded', 'Budget', 'Timeline'
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE()
);

-- ---------------------------------------------------------------------------------
-- Table: Leads
-- Purpose: Stores information about potential customers.
-- Why: The central entity for all sales activities. It tracks lead details,
-- their current stage in the sales pipeline, and their overall status.
-- ---------------------------------------------------------------------------------
CREATE TABLE Leads (
    LeadId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    FirstName NVARCHAR(100),
    LastName NVARCHAR(100),
    Email NVARCHAR(255) UNIQUE,
    PhoneNumber NVARCHAR(50) UNIQUE,
    CompanyName NVARCHAR(200),
    Source NVARCHAR(100), -- e.g., 'Website Form', 'Manual Import', 'API'
    Status NVARCHAR(50) NOT NULL, -- e.g., 'New', 'Contacted', 'Qualified', 'Unqualified', 'Converted'
    SalesStage NVARCHAR(50) NOT NULL, -- e.g., 'InitialContact', 'NeedsAnalysis', 'Proposal', 'Closed'
    LeadScore INT DEFAULT 0,
    AssignedTo NVARCHAR(100) DEFAULT 'AI', -- Could be 'AI' or a human user's ID
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE()
);

-- ---------------------------------------------------------------------------------
-- Table: SalesTasks
-- Purpose: Manages the state of background sales tasks for each lead.
-- Why: This is the backbone of the asynchronous workflow. It allows the system
-- to track long-running processes (e.g., a 7-day follow-up sequence) and
-- ensures the AI can resume its work with full context, even after long delays.
-- ---------------------------------------------------------------------------------
CREATE TABLE SalesTasks (
    TaskId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    LeadId UNIQUEIDENTIFIER NOT NULL,
    TaskType NVARCHAR(100) NOT NULL, -- e.g., 'InitialOutreach', 'FollowUp', 'NurturingSequence'
    TaskState NVARCHAR(50) NOT NULL, -- 'PENDING', 'RUNNING', 'WAITING_FOR_CLIENT', 'ESCALATED', 'COMPLETED'
    ScheduledAt DATETIME2,
    CompletedAt DATETIME2,
    FailureReason NVARCHAR(MAX),
    NextActionAt DATETIME2,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    FOREIGN KEY (LeadId) REFERENCES Leads(LeadId) ON DELETE CASCADE
);

-- ---------------------------------------------------------------------------------
-- Table: SalesTimeline
-- Purpose: Records every single interaction and event for a lead.
-- Why: Provides a full, chronological history of the sales process. This is
-- crucial for both the AI (to have context) and the founder (for visibility).
-- It's the source of truth for the lead detail view.
-- ---------------------------------------------------------------------------------
CREATE TABLE SalesTimeline (
    EventId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    LeadId UNIQUEIDENTIFIER NOT NULL,
    EventType NVARCHAR(100) NOT NULL, -- e.g., 'EmailSent', 'AIResponseReceived', 'StatusChange', 'HumanTakeover'
    Content NVARCHAR(MAX),
    SourceChannel NVARCHAR(50), -- 'Email', 'WhatsApp', 'WebChat', 'System'
    Actor NVARCHAR(100), -- 'AI', 'System', or a human user's name/ID
    EventTimestamp DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    FOREIGN KEY (LeadId) REFERENCES Leads(LeadId) ON DELETE CASCADE
);

-- ---------------------------------------------------------------------------------
-- Table: AIResponses
-- Purpose: Stores the raw prompts and responses from the AI model.
-- Why: Essential for debugging, auditing, and fine-tuning. By storing the exact
-- prompt sent and response received, we can analyze the AI's performance and
-- understand its decision-making process.
-- ---------------------------------------------------------------------------------
CREATE TABLE AIResponses (
    ResponseId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    TimelineEventId UNIQUEIDENTIFIER,
    CompiledPrompt NVARCHAR(MAX) NOT NULL,
    ResponseText NVARCHAR(MAX) NOT NULL,
    ModelUsed NVARCHAR(100), -- e.g., 'gpt-4-turbo'
    ProcessingTimeMs INT,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    FOREIGN KEY (TimelineEventId) REFERENCES SalesTimeline(EventId)
);

-- ---------------------------------------------------------------------------------
-- Table: EscalationRules
-- Purpose: Defines the conditions under which the AI must escalate to a human.
-- Why: This is the core of the human-in-the-loop design. It allows the founder
-- to set rules (e.g., based on deal value, keywords, or sentiment) that
-- automatically pause the AI and notify the team, ensuring critical
-- conversations get the attention they need.
-- ---------------------------------------------------------------------------------
CREATE TABLE EscalationRules (
    RuleId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    RuleName NVARCHAR(200) NOT NULL,
    ConditionType NVARCHAR(100) NOT NULL, -- e.g., 'Keyword', 'Sentiment', 'DealValue', 'Manual'
    ConditionValue NVARCHAR(500) NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE()
);

-- ---------------------------------------------------------------------------------
-- Table: Objections
-- Purpose: Stores a library of common sales objections and their approved responses.
-- Why: Allows the founder to train the AI on how to handle pushback by providing
-- a pre-approved set of counter-arguments and responses.
-- ---------------------------------------------------------------------------------
CREATE TABLE Objections (
    ObjectionId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    ObjectionName NVARCHAR(200) NOT NULL,
    Keywords NVARCHAR(MAX),
    ResponseText NVARCHAR(MAX) NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE()
);

