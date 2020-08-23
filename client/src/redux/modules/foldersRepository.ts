import { FoldersRepository } from "../../repositories/FoldersRepository";

// State
const initialFoldersRepository: FoldersRepository = new FoldersRepository();

// Reducers
export function foldersRepositoryReducer(state = initialFoldersRepository, action: any) {
    return state;
}