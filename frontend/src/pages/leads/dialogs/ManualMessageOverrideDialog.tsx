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
import { Textarea } from "@/components/ui/textarea";

const ManualMessageOverrideDialog: React.FC = () => {
  return (
    <Dialog>
      <DialogTrigger asChild>
        <Button variant="outline">Manual Message</Button>
      </DialogTrigger>
      <DialogContent>
        <DialogHeader>
          <DialogTitle>Manual Message Override</DialogTitle>
          <DialogDescription>
            Send a manual message to the lead. This will pause the AI.
          </DialogDescription>
        </DialogHeader>
        <Textarea placeholder="Type your message here." />
        <div className="flex justify-end gap-2 mt-4">
          <Button variant="outline">Cancel</Button>
          <Button>Send</Button>
        </div>
      </DialogContent>
    </Dialog>
  );
};

export default ManualMessageOverrideDialog;
