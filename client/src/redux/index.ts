import { combineReducers } from 'redux';
import { foldersReducer } from './modules/folders';
import { foldersRepositoryReducer } from './modules/foldersRepository';
import { imagesRepositoryReducer } from './modules/imagesRepository';
import { imagesReducer } from './modules/images';

export const rootReducer = combineReducers({
    folders: foldersReducer,
    foldersRepository: foldersRepositoryReducer,
    imagesRepository: imagesRepositoryReducer,
    selected_image: imagesReducer
});

export type RootState = ReturnType<typeof rootReducer>;