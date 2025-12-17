import React from 'react';
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";

const LeadProfile: React.FC = () => {
  // Dummy data for now
  const lead = {
    name: 'John Doe',
    email: 'john.doe@example.com',
    phone: '123-456-7890',
    source: 'LinkedIn',
    service: 'Web Development',
  };

  return (
    <Card>
      <CardHeader>
        <CardTitle>Lead Profile</CardTitle>
      </CardHeader>
      <CardContent>
        <p><strong>Name:</strong> {lead.name}</p>
        <p><strong>Email:</strong> {lead.email}</p>
        <p><strong>Phone:</strong> {lead.phone}</p>
        <p><strong>Source:</strong> {lead.source}</p>
        <p><strong>Service:</strong> {lead.service}</p>
      </CardContent>
    </Card>
  );
};

export default LeadProfile;
