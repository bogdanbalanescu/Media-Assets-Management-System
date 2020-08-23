import { combineReducers } from 'redux';
import { foldersReducer } from './modules/folders';
import { foldersRepositoryReducer } from './modules/foldersRepository';

export const rootReducer = combineReducers({
    folders: foldersReducer,
    foldersRepository: foldersRepositoryReducer
});

export type RootState = ReturnType<typeof rootReducer>;