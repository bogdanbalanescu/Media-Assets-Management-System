export class Image {
    id: number;
    name: string;
    folderId: number;

    public constructor(id: number, name: string, folderId: number) {
        this.id = id;
        this.name = name;
        this.folderId = folderId;
    }
}