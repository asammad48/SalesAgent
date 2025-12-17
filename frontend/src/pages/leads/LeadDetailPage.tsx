import React from 'react';
import { useParams } from 'react-router-dom';
import PageHeader from '@/app/layout/PageHeader';
import LeadProfile from './LeadProfile';
import AiStageIndicator from './AiStageIndicator';
import ConversationTimeline from './ConversationTimeline';
import SalesTaskStatus from './SalesTaskStatus';
import EditLeadInfoDialog from './dialogs/EditLeadInfoDialog';
import ViewAiPromptDialog from './dialogs/ViewAiPromptDialog';
import EscalateToHumanDialog from './dialogs/EscalateToHumanDialog';
import PauseResumeAiDialog from './dialogs/PauseResumeAiDialog';
import ManualMessageOverrideDialog from './dialogs/ManualMessageOverrideDialog';

const LeadDetailPage: React.FC = () => {
  const { leadId } = useParams<{ leadId: string }>();

  // Dummy data for now
  const leadName = 'John Doe';

  return (
    <div>
      <PageHeader
        title={`Lead: ${leadName}`}
        actions={
          <div className="flex gap-2">
            <EditLeadInfoDialog />
            <ViewAiPromptDialog />
            <EscalateToHumanDialog />
            <PauseResumeAiDialog />
            <ManualMessageOverrideDialog />
          </div>
        }
      />
      <div className="grid grid-cols-1 lg:grid-cols-3 gap-6">
        <div className="lg:col-span-1 space-y-6">
          <LeadProfile />
          <AiStageIndicator />
          <SalesTaskStatus />
        </div>
        <div className="lg:col-span-2">
          <ConversationTimeline />
        </div>
      </div>
    </div>
  );
};

export default LeadDetailPage;
