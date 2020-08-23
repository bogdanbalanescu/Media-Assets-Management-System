// State
const initialImagesState: number = -1;

// Actions
const SELECT_IMAGE = 'SELECT_IMAGE';
interface SelectImageAction {
    type: typeof SELECT_IMAGE,
    id: number
}

type ImagesActions = SelectImageAction;

// Action Creators
export function selectImage(id: number): ImagesActions {
    return {
        type: SELECT_IMAGE,
        id: id
    }
}

// Reducers
export function imagesReducer(state = initialImagesState, action: ImagesActions) {
    switch (action.type) {
        case SELECT_IMAGE:
            return action.id;
        default:
            return state;
    };
}