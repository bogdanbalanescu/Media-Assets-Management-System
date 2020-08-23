import { Folder } from "../../models/Folder";
import { Image } from "../../models/Image";

// State
const initialFoldersState: Folder[] = [];

// Actions
const ADD_FOLDERS = 'ADD_FOLDERS';
interface AddFoldersAction {
    type: typeof ADD_FOLDERS,
    folders: Folder[]
}

const ADD_IMAGES = 'ADD_IMAGES';
interface AddImagesAction {
    type: typeof ADD_IMAGES,
    images: Image[]
}

type FoldersActions = AddFoldersAction | AddImagesAction;

// Action Creators
export function addFolders(folders: Folder[]): FoldersActions {
    return {
        type: ADD_FOLDERS,
        folders: folders
    }
}
export function addImages(images: Image[]): FoldersActions {
    return {
        type: ADD_IMAGES,
        images: images
    };
}

// Helper functions
function findParentFolderById(folders: Folder[], parentId: number) {
    var parentFolderContainer = folders.find(x => x.HasFolderId(parentId));
    if (parentFolderContainer !== undefined) {
        var flatListOfParentsChildren = parentFolderContainer.GetFlatListOfChildren();
        return flatListOfParentsChildren.find(x => x.id === parentId);
    }
    else console.log('A parent which does not exist has been referenced (1):', parentId);
}

// Reducers
export function foldersReducer(state = initialFoldersState, action: FoldersActions) {
    var currentFolders: Folder[] = [];
    switch (action.type) {
        case ADD_FOLDERS:
            currentFolders = [...state];
            action.folders.forEach(folder => {
                if (folder.parentId) {
                    var parentFolder = findParentFolderById(currentFolders, folder.parentId);
                    if (parentFolder !== undefined) parentFolder.children.push(folder);
                    else console.log('One folder referenced a parent which does not exist (2):', folder);
                }
                else {
                    currentFolders.push(folder);
                }
            });
            return currentFolders;
        case ADD_IMAGES:
            currentFolders = [...state];
            action.images.forEach(image => {
                var parentFolder = findParentFolderById(currentFolders, image.folderId);
                if (parentFolder !== undefined) parentFolder.images.push(image);
                else console.log('One image referenced a parent which does not exist (2):', image);
            })
            return currentFolders;
        default:
            return state;
    }
}