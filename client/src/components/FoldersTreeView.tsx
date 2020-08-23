import React from 'react';
import TreeView from '@material-ui/lab/TreeView';
import ArrowDropDownIcon from '@material-ui/icons/ArrowDropDown';
import ArrowRightIcon from '@material-ui/icons/ArrowRight';
import FolderIcon from '@material-ui/icons/Folder'
import { connect } from 'react-redux';
import { RootState } from '../redux';
import { Folder } from '../models/Folder';
import { addFolders } from '../redux/modules/folders';
import StyledTreeItem from './StyledTreeItem';
import NewFolderDialog from './NewFolderDialog';

const mapStateToProps = (state: RootState) => ({
    folders: state.folders,
    foldersRepository: state.foldersRepository
  });
const mapDispatchToProps = { 
    addFolders: addFolders
};

type Props = ReturnType<typeof mapStateToProps> & typeof mapDispatchToProps;
  
class FoldersTreeView extends React.Component<Props> {
    componentDidMount = () => {
        this.props.foldersRepository
            .GetFolders()
            .then((response) => {
                this.props.addFolders(response);
                response.forEach(folder => {
                    this.onFolderClick(folder);
                })
            })
            .catch((error) => {
                console.log(error);
            });
    }
    
    onFolderClick = (folder: Folder) => {
        if (folder.children.length > 0) return;
        this.props.foldersRepository
            .GetSubfolders(folder.id)
            .then((response) => {
                this.props.addFolders(response);
                response.forEach(folder => {
                    this.onFolderClick(folder);
                })
            })
            .catch((error) => {
                console.log(error);
            });
    }

    handleCreateFolder = (folderName: string, parentId: number) => {
        if (parentId !== 0) {
            this.props.foldersRepository
                .CreateSubfolder(folderName, parentId)
                .then((response) => {
                    this.props.addFolders([response]);
                })
                .catch((error) => {
                    console.log(error);
                });
        }
        else {
            this.props.foldersRepository
                .CreateFolder(folderName)
                .then((response) => {
                    this.props.addFolders([response]);
                })
                .catch((error) => {
                    console.log(error);
                });
        }
    }

    mapFolder = (folder: Folder): JSX.Element => {
        return (
            <StyledTreeItem onClick={() => this.onFolderClick(folder)} nodeId={folder.id.toString()} labelText={folder.name} labelIcon={FolderIcon}>
                {folder.children.map(this.mapFolder)}
                <NewFolderDialog parentId={folder.id} onPostFolder={this.handleCreateFolder}/>
            </StyledTreeItem>
        );
    }

    render = () => {
        return (
            <TreeView
                defaultCollapseIcon={<ArrowDropDownIcon />}
                defaultExpandIcon={<ArrowRightIcon />}
                defaultEndIcon={<div style={{ width: 24 }} />}
            >
                {
                    this.props.folders.map(this.mapFolder)
                }
                <NewFolderDialog parentId={0} onPostFolder={this.handleCreateFolder}/>
            </TreeView>
        );
  }
}

const connector = connect(mapStateToProps, mapDispatchToProps);
export default connector(FoldersTreeView);
