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

const QuickViewLeadDialog: React.FC = () => {
  return (
    <Dialog>
      <DialogTrigger asChild>
        <Button variant="outline">Quick View</Button>
      </DialogTrigger>
      <DialogContent>
        <DialogHeader>
          <DialogTitle>Lead Summary</DialogTitle>
          <DialogDescription>
            A quick overview of the lead's information.
          </DialogDescription>
        </DialogHeader>
        {/* Lead summary content will go here */}
      </DialogContent>
    </Dialog>
  );
};

export default QuickViewLeadDialog;
