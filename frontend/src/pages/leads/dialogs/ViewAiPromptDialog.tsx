import React from 'react';
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from "@/components/ui/dialog";
import { Button } from "@/components/ui/button";

const ViewAiPromptDialog: React.FC = () => {
  return (
    <Dialog>
      <DialogTrigger asChild>
        <Button variant="outline">View AI Prompt</Button>
      </DialogTrigger>
      <DialogContent>
        <DialogHeader>
          <DialogTitle>AI Prompt Used</DialogTitle>
          <DialogDescription>
            The prompt that the AI is currently using for this lead.
          </DialogDescription>
        </DialogHeader>
        {/* AI prompt content will go here */}
      </DialogContent>
    </Dialog>
  );
};

export default ViewAiPromptDialog;
