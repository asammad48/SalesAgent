import React from 'react';
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";
import { Badge } from "@/components/ui/badge";

const SalesTaskStatus: React.FC = () => {
  // Dummy data for now
  const taskStatus = 'Running';

  return (
    <Card>
      <CardHeader>
        <CardTitle>Sales Task Status</CardTitle>
      </Header>
      <CardContent>
        <Badge>{taskStatus}</Badge>
      </CardContent>
    </Card>
  );
};

export default SalesTaskStatus;
