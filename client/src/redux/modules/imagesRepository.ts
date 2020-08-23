import { ImagesRepository } from "../../repositories/ImagesRepository";

// State
const initialImagesRepository: ImagesRepository = new ImagesRepository();

// Reducers
export function imagesRepositoryReducer(state = initialImagesRepository, action: any) {
    return state;
}