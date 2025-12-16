# AI Sales Agent - System Architecture & Design

This document outlines the complete architecture, design, and workflow for the AI Sales Agent system.

## 1. System Architecture

### High-Level Architecture Diagram

```
+----------------------------------------------------------------------------------+
|                             END USER (Founder / Sales Team)                      |
|                                (Web Browser)                                     |
+----------------------------------------------------------------------------------+
                                        |
                                        | (HTTPS)
                                        v
+----------------------------------------------------------------------------------+
|                                  FRONTEND                                        |
|                       (Single Page Application - e.g., React)                    |
|----------------------------------------------------------------------------------|
| [Dashboard] [Pipeline] [Lead Detail] [Service & Pitch Editor] [Agent Settings]   |
+----------------------------------------------------------------------------------+
                                        |
                                        | (RESTful API Calls - JSON)
                                        v
+----------------------------------------------------------------------------------+
|                                   BACKEND                                        |
|                              (.NET 8 Web API)                                    |
|----------------------------------------------------------------------------------|
|                                   API Layer (Controllers)                        |
|                                                                                  |
|                +--------------------------------------------------+              |
|                |              Application Core (Services)         |              |
|                |                                                  |              |
|                |  [Sales Orchestrator]  [Prompt Builder Service]  |              |
|                |                                                  |              |
|                +--------------------------------------------------+              |
|                      |                       |                       |           |
|  (DI)                |  (DI)                 |  (DI)                 |  (DI)     |
|   v                  v                       v                       v           |
| +------------------+ +---------------------+ +---------------------+ +-----------+ |
| | Persistence      | | Background Tasks    | | AI Layer            | | External  | |
| | (Repositories)   | | (Task Engine)       | | (Integrations)      | | APIs      | |
| +------------------+ +---------------------+ +---------------------+ +-----------+ |
+----------------------------------------------------------------------------------+
     |                  |                      |                       |
     | (EF Core)        | (Enqueues Jobs)      | (API Call)            | (API Call)
     v                  v                      v                       v
+----------+   +-------------------+  +-------------------+  +----------------------+
| DATABASE |   | BACKGROUND WORKER |  | OpenAI GPT API    |  | WhatsApp & Email APIs|
| (SQL     |   | (Hangfire)        |  +-------------------+  +----------------------+
| Server)  |   +-------------------+
+----------+
```

### Component Explanations

| Component | Technology | Role |
| :--- | :--- | :--- |
| **Frontend** | React / Vue / Angular | Provides the UI for the founder. It communicates with the backend via RESTful APIs and is responsible for all visualization and user interaction. |
| **Backend API** | .NET 8 Web API | The central system hub. It exposes endpoints, handles business logic, and coordinates all backend components using Clean Architecture. |
| ↳ **Application Core** | .NET 8 Class Library | Contains core business logic, including the `Sales Orchestrator` and `Prompt Builder`. It is independent of UI and database. |
| ↳ **Persistence** | Entity Framework Core | Implements the repository pattern for all database operations, abstracting data access from the Application Core. |
| ↳ **Background Tasks**| Hangfire | A dedicated engine for running long-running, asynchronous tasks like lead nurturing and follow-ups. |
| **AI Layer** | OpenAI GPT Integration | A dedicated service to handle all communication with the external language model (e.g., GPT-4). |
| **Database** | SQL Server / PostgreSQL | The single source of truth for all application data. |
| **External Integrations** | Twilio (WhatsApp), SendGrid (Email) | Third-party services for multi-channel communication, abstracted behind interfaces. |

---

## 2. Backend Design

### Folder Structure
The backend follows Clean Architecture principles, organized into distinct projects:

```
/
├── src/
│   ├── API/
│   │   └── AISalesAgent.WebAPI/
│   ├── Core/
│   │   ├── AISalesAgent.Application/
│   │   └── AISalesAgent.Domain/
│   └── Infrastructure/
│       ├── AISalesAgent.Infrastructure/
│       └── AISalesAgent.Persistence/
└── tests/
```

### Background Task Workflow
The system uses a stateful workflow for managing long-running sales tasks, tracked in the `SalesTasks` database table.

**Task States:** `PENDING`, `RUNNING`, `WAITING_FOR_CLIENT`, `ESCALATED`, `COMPLETED`, `FAILED`.

**Workflow Diagram:**
```
[New Lead] -> [PENDING Task] -> (Scheduler) -> [RUNNING] -> (AI Action) -> [WAITING_FOR_CLIENT]
                                                                                ^
                                                                                |
(Client Reply or Follow-up Timer) -> (New PENDING Task) -----------------------+
```

---

## 3. Frontend Layout

The UI is designed for **"Clarity and Control,"** allowing the founder to understand the pipeline and intervene at any time.

### Navigation
A persistent left-hand navigation bar provides access to:
- Dashboard
- Pipeline
- Leads
- AI Task Monitor
- Settings (Services, Objections, Agent)

### Key Screens
- **Dashboard:** High-level KPIs, sales funnel, and AI task status.
- **Pipeline / Kanban Board:** Visual, draggable pipeline of leads.
- **Lead Detail Page:** A three-column view showing lead info, a unified conversation timeline, and human-takeover controls.
- **Services & Pitch Editor:** UI-driven editor for all AI sales copy.
- **Agent Settings:** A form to define the AI's core persona and behavior.

---

## 4. Prompt Compilation & Logic

The `PromptBuilderService` dynamically assembles context-rich prompts from the database.

**Prompt Structure:**
1.  **System Prompt:** Core AI persona from `AgentSettings`.
2.  **Task Block:** The immediate goal for the interaction.
3.  **Lead Context Block:** Lead details and full conversation history from `Leads` and `SalesTimeline`.
4.  **Knowledge Resource Block:** The approved sales copy from `Services`, `ServicePitches`, `ConversationQuestions`, and `CTAs`.
5.  **Output Formatting:** Strict instructions for the AI to return a JSON object.

---

## 5. Human-in-the-Loop & Multi-Channel Integration

### Escalation Rules
The AI automatically pauses and escalates to a human (`ESCALATED` state) under predefined conditions stored in `EscalationRules`:
- High-Value Deals
- Pricing Negotiation
- Legal & Security Keywords
- Negative Client Sentiment
- Manual Override by Founder

### Multi-Channel State Management
The system uses a **centralized state model** where the `SalesTimeline` table is the single source of truth.
- Webhooks from Email, WhatsApp, etc., all write to the central timeline.
- The AI reads the unified timeline to get full context.
- Outbound messages are sent on the channel the lead last used or their preferred channel.

---

## 6. Analytics & Marketing

### Dashboards
- **KPIs:** Total Leads, Qualified Leads, Meetings Booked, Conversion Rate.
- **AI Performance:** Tasks Completed, Human Escalations, AI vs. Human Win Rate.
- **Sales Funnel:** Lead stage drop-off analysis and ROI per service.
