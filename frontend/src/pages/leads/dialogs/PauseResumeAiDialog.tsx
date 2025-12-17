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

const PauseResumeAiDialog: React.FC = () => {
  // Dummy state for now
  const isAiPaused = false;

  return (
    <Dialog>
      <DialogTrigger asChild>
        <Button variant="outline">{isAiPaused ? 'Resume AI' : 'Pause AI'}</Button>
      </DialogTrigger>
      <DialogContent>
        <DialogHeader>
          <DialogTitle>Are you sure?</DialogTitle>
          <DialogDescription>
            This will {isAiPaused ? 'resume' : 'pause'} the AI for this lead.
          </DialogDescription>
        </DialogHeader>
        <div className="flex justify-end gap-2 mt-4">
          <Button variant="outline">Cancel</Button>
          <Button>{isAiPaused ? 'Resume' : 'Pause'}</Button>
        </div>
      </DialogContent>
    </Dialog>
  );
};

export default PauseResumeAiDialog;
