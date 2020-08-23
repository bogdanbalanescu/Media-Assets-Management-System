export class Folder {
    id: number;
    name: string;
    children: Folder[];
    parentId?: number;

    public constructor(id: number, name: string, children?: Folder[], parentId?: number) {
        this.id = id;
        this.name = name;
        this.children = children ? children : [];
        this.parentId = parentId;
    }
}