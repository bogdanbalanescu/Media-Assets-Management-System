import { Image } from "./Image";

export class Folder {
    id: number;
    name: string;
    children: Folder[];
    parentId?: number;
    images: Image[];

    public constructor(id: number, name: string, children?: Folder[], parentId?: number, images?: Image[]) {
        this.id = id;
        this.name = name;
        this.children = children ? children : [];
        this.parentId = parentId;
        this.images = images ? images : [];
    }

    GetFlatListOfChildren(): Folder[] {
        var flatListOfChildren: Folder[] = [this, ...this.children];
        this.children.forEach(x => flatListOfChildren.push(...x.GetFlatListOfChildren()));
        return flatListOfChildren;
    }
    HasFolderId(parentId?: number): boolean {
        if (this.id === parentId) return true;
        return this.children.findIndex(x => x.HasFolderId(parentId)) !== -1;
    }
}