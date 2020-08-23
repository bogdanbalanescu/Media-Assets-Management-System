import React from 'react';
import TreeView from '@material-ui/lab/TreeView';
import ArrowDropDownIcon from '@material-ui/icons/ArrowDropDown';
import ArrowRightIcon from '@material-ui/icons/ArrowRight';
import FolderIcon from '@material-ui/icons/Folder'
import ImageIcon from '@material-ui/icons/Image'
import { connect } from 'react-redux';
import { RootState } from '../redux';
import { Folder } from '../models/Folder';
import { Image } from '../models/Image';
import { addFolders, addImages } from '../redux/modules/folders';
import StyledTreeItem from './StyledTreeItem';
import NewFolderDialog from './NewFolderDialog';
import NewImageDialog from './NewImageDialog';
import { selectImage } from '../redux/modules/images';

const mapStateToProps = (state: RootState) => ({
    folders: state.folders,
    foldersRepository: state.foldersRepository,
    imagesRepository: state.imagesRepository
  });
const mapDispatchToProps = { 
    addFolders: addFolders,
    addImages: addImages,
    selectImage: selectImage
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
        this.props.imagesRepository
            .GetImagesForFolder(folder.id)
            .then((response) => {
                this.props.addImages(response);
            })
            .catch((error) => {
                console.log(error);
            });
    }

    onImageClick = (image: Image) => {
        this.props.selectImage(image.id);
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

    handleCreateImage = (image: any, folderId: number) => {
        this.props.imagesRepository
            .CreateImage(image, folderId)
            .then((response) => {
                this.props.addImages([response]);
            })
            .catch((error) => {
                console.log(error);
            });
    }

    mapFolder = (folder: Folder): JSX.Element => {
        return (
            <StyledTreeItem onClick={() => this.onFolderClick(folder)} nodeId={`${folder.id.toString()}_folder`} labelText={folder.name} labelIcon={FolderIcon}>
                <NewFolderDialog parentId={folder.id} onPostFolder={this.handleCreateFolder}/>
                <NewImageDialog folderId={folder.id} onPostImage={this.handleCreateImage}/>
                {folder.children.map(this.mapFolder)}
                {folder.images.map(image => {
                    return (
                        <StyledTreeItem onClick={() => this.onImageClick(image)} nodeId={`${image.id.toString()}_image`} labelText={image.name} labelIcon={ImageIcon}>
                        </StyledTreeItem>
                    );
                })}
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
                <NewFolderDialog parentId={0} onPostFolder={this.handleCreateFolder}/>
                {
                    this.props.folders.map(this.mapFolder)
                }
            </TreeView>
        );
  }
}

const connector = connect(mapStateToProps, mapDispatchToProps);
export default connector(FoldersTreeView);
