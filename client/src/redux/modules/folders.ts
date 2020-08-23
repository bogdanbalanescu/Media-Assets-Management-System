import { Folder } from "../../models/Folder";

// State
const initialFoldersState: Folder[] = [];

// Actions
const  ADD_FOLDERS = 'ADD_FOLDERS';
interface AddFoldersAction {
    type: typeof ADD_FOLDERS,
    folders: Folder[]
}

type FoldersActions = AddFoldersAction;

// Action Creators
export function addFolders(folders: Folder[]): FoldersActions {
    return {
        type: ADD_FOLDERS,
        folders: folders
    }
}

// Reducers
export function foldersReducer(state = initialFoldersState, action: FoldersActions) {
    switch (action.type) {
        case ADD_FOLDERS:
            var currentFolders = [...state];
            action.folders.forEach(folder => {
                if (folder.parentId) {
                    var parentFolder = currentFolders.find(x => x.id === folder.parentId);
                    if (parentFolder !== undefined) parentFolder.children.push(folder);
                    else console.log('One folder referenced a parent which does not exist:', folder);
                }
                else {
                    currentFolders.push(folder);
                }
            });
            return currentFolders;
        default:
            return state;
    }
}