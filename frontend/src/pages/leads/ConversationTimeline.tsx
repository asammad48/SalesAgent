import React from 'react';
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar";

const ConversationTimeline: React.FC = () => {
  // Dummy data for now
  const timeline = [
    { type: 'message', sender: 'AI', text: 'Hello, are you interested in our services?' },
    { type: 'message', sender: 'Lead', text: 'Yes, tell me more.' },
    { type: 'action', sender: 'AI', text: 'Sent a follow-up email.' },
  ];

  return (
    <Card>
      <CardHeader>
        <CardTitle>Conversation Timeline</CardTitle>
      </CardHeader>
      <CardContent>
        <div className="space-y-4">
          {timeline.map((item, index) => (
            <div key={index} className="flex items-start gap-4">
              <Avatar>
                <AvatarImage src={item.sender === 'AI' ? '/ai-avatar.png' : '/lead-avatar.png'} />
                <AvatarFallback>{item.sender === 'AI' ? 'AI' : 'L'}</AvatarFallback>
              </Avatar>
              <div className="flex-1">
                <p className="font-semibold">{item.sender}</p>
                <p>{item.text}</p>
              </div>
            </div>
          ))}
        </div>
      </CardContent>
    </Card>
  );
};

export default ConversationTimeline;
