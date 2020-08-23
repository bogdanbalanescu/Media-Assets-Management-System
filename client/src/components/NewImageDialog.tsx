import React from 'react';
import Button from '@material-ui/core/Button';
import AddAPhotoIcon from '@material-ui/icons/AddAPhoto'
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogContentText from '@material-ui/core/DialogContentText';
import DialogTitle from '@material-ui/core/DialogTitle';
import StyledTreeItem from './StyledTreeItem';

interface CreateFolderProps {
    folderId: number;
    onPostImage: (image: any, folderId: number) => void;
}

interface CreateFolderState {
    open: boolean;
    image: any
}

export default class NewImageDialog extends React.Component<CreateFolderProps, CreateFolderState> {
    constructor(props: CreateFolderProps) {
        super(props);

        this.state = {
            open: false,
            image: null
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

        if (shouldCreate) this.props.onPostImage(this.state.image, this.props.folderId);
    };

    handleImageChange = (e: any) => {
        this.setState({
            ...this.state,
            image: e.target.files[0]
        });
    }

    render = () => {
        return (
            <div>
                <StyledTreeItem onClick={this.handleClickOpen} nodeId={`${this.props.folderId.toString()}x`} labelText="New Image" labelIcon={AddAPhotoIcon}>
                </StyledTreeItem>
                <Dialog open={this.state.open} onClose={this.handleClose} aria-labelledby="form-dialog-title">
                <DialogTitle id="form-dialog-title">Create New Image</DialogTitle>
                <DialogContent>
                    <DialogContentText>
                    To create a new image, please upload a new image here.
                    </DialogContentText>
                    <Button variant="contained" component="label">
                        Upload File
                        <input
                            type="file"
                            onChange={e => this.handleImageChange(e)}
                        />
                    </Button>
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
