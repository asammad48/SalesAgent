import React from 'react';
import PageHeader from '@/app/layout/PageHeader';
import AddLeadDialog from './AddLeadDialog';
import LeadsTable from './LeadsTable';

const LeadsPage: React.FC = () => {
  return (
    <div>
      <PageHeader
        title="Leads"
        actions={<AddLeadDialog />}
      />
      <LeadsTable />
    </div>
  );
};

export default LeadsPage;
