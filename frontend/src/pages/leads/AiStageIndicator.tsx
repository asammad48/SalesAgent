import React from 'react';
import { Badge } from "@/components/ui/badge";
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card';

const AiStageIndicator: React.FC = () => {
  // Dummy data for now
  const aiStages = ['Discovery', 'Pitch', 'Follow-up', 'Close'];
  const currentStage = 'Pitch';

  return (
    <Card>
      <CardHeader>
        <CardTitle>AI Stage</CardTitle>
      </CardHeader>
      <CardContent>
        <div className="flex justify-between">
          {aiStages.map((stage, index) => (
            <div key={stage} className="flex flex-col items-center">
              <Badge variant={currentStage === stage ? 'default' : 'secondary'}>
                {stage}
              </Badge>
            </div>
          ))}
        </div>
      </CardContent>
    </Card>
  );
};

export default AiStageIndicator;
