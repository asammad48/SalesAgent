import React from 'react';
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";
import QuickViewLeadDialog from './QuickViewLeadDialog';
import ForceAiRerunDialog from './ForceAiRerunDialog';
import DeleteLeadDialog from './DeleteLeadDialog';

const LeadsTable: React.FC = () => {
  // Dummy data for now
  const leads = [
    { name: 'John Doe', source: 'LinkedIn', service: 'Web Development', aiStage: 'Discovery', qualificationScore: 85, status: 'Active', createdDate: '2024-07-20' },
    { name: 'Jane Smith', source: 'Website', service: 'SEO', aiStage: 'Pitch', qualificationScore: 92, status: 'Contacted', createdDate: '2024-07-19' },
  ];

  return (
    <Table>
      <TableHeader>
        <TableRow>
          <TableHead>Lead Name</TableHead>
          <TableHead>Source</TableHead>
          <TableHead>Service</TableHead>
          <TableHead>AI Stage</TableHead>
          <TableHead>Qualification Score</TableHead>
          <TableHead>Status</TableHead>
          <TableHead>Created Date</TableHead>
          <TableHead>Actions</TableHead>
        </TableRow>
      </TableHeader>
      <TableBody>
        {leads.map((lead, index) => (
          <TableRow key={index}>
            <TableCell>{lead.name}</TableCell>
            <TableCell>{lead.source}</TableCell>
            <TableCell>{lead.service}</TableCell>
            <TableCell>{lead.aiStage}</TableCell>
            <TableCell>{lead.qualificationScore}</TableCell>
            <TableCell>{lead.status}</TableCell>
            <TableCell>{lead.createdDate}</TableCell>
            <TableCell>
              <div className="flex gap-2">
                <QuickViewLeadDialog />
                <ForceAiRerunDialog />
                <DeleteLeadDialog />
              </div>
            </TableCell>
          </TableRow>
        ))}
      </TableBody>
    </Table>
  );
};

export default LeadsTable;
