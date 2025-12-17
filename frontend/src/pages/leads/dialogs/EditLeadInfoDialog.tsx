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

const EditLeadInfoDialog: React.FC = () => {
  return (
    <Dialog>
      <DialogTrigger asChild>
        <Button variant="outline">Edit Lead</Button>
      </DialogTrigger>
      <DialogContent>
        <DialogHeader>
          <DialogTitle>Edit Lead Information</DialogTitle>
          <DialogDescription>
            Update the lead's details.
          </DialogDescription>
        </DialogHeader>
        {/* Edit lead form will go here */}
      </DialogContent>
    </Dialog>
  );
};

export default EditLeadInfoDialog;
