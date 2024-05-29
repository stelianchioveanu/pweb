import { Button, Dialog, DialogContent, DialogTitle } from "@mui/material";
import { useTagAddDialogController } from "./TagAddDialog.controller";
import { TagAddForm } from "@presentation/components/forms/Tags/TagAddForm";
import { useIntl } from "react-intl";

/**
 * This component wraps the user add form into a modal dialog.
 */
export const TagAddDialog = () => {
  const { open, close, isOpen } = useTagAddDialogController();
  const { formatMessage } = useIntl();

  return <div>
    <Button variant="outlined" onClick={open}>
      {formatMessage({ id: "labels.addTag" })}
    </Button>
    <Dialog
      open={isOpen}
      onClose={close}>
      <DialogTitle>
        {formatMessage({ id: "labels.addTag" })}
      </DialogTitle>
      <DialogContent>
        <TagAddForm onSubmit={close} />
      </DialogContent>
    </Dialog>
  </div>
};