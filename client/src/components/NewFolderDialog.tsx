import React from 'react';
import Button from '@material-ui/core/Button';
import TextField from '@material-ui/core/TextField';
import CreateNewFolderIcon from '@material-ui/icons/CreateNewFolder'
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogContentText from '@material-ui/core/DialogContentText';
import DialogTitle from '@material-ui/core/DialogTitle';
import StyledTreeItem from './StyledTreeItem';

interface CreateFolderProps {
    parentId: number;
    onPostFolder: (folderName: string, parentId: number) => void;
}

interface CreateFolderState {
    open: boolean;
    text: string
}

export default class NewFolderDialog extends React.Component<CreateFolderProps, CreateFolderState> {
    constructor(props: CreateFolderProps) {
        super(props);

        this.state = {
            open: false,
            text: ""
        };
    }

    handleClickOpen = () => {
        this.setState({
            ...this.state,
            open: true
        })
    };

    handleClose = (shouldCreate: boolean) => {
        this.setState({
            ...this.state,
            open: false
        })
        if (shouldCreate) this.props.onPostFolder(this.state.text, this.props.parentId);
    };

    handleTextChange = (e: any) => {
        this.setState({
            ...this.state,
            text: e.target.value
        });
    }

    render = () => {
        return (
            <div>
                <StyledTreeItem onClick={this.handleClickOpen} nodeId={`${this.props.parentId.toString()}x`} labelText="New Folder" labelIcon={CreateNewFolderIcon}>
                </StyledTreeItem>
                <Dialog open={this.state.open} onClose={this.handleClose} aria-labelledby="form-dialog-title">
                <DialogTitle id="form-dialog-title">Create New Folder</DialogTitle>
                <DialogContent>
                    <DialogContentText>
                    To create a new folder, please enter the name of the new folder here.
                    </DialogContentText>
                    <TextField
                        autoFocus
                        margin="dense"
                        id="name"
                        label="New Folder Name"
                        type="string"
                        fullWidth
                        onChange={e => this.handleTextChange(e)}
                    />
                </DialogContent>
                <DialogActions>
                    <Button onClick={() => this.handleClose(false)} color="primary">
                    Cancel
                    </Button>
                    <Button onClick={() => this.handleClose(true)} color="primary">
                    Create
                    </Button>
                </DialogActions>
                </Dialog>
            </div>
        );
    }
}
