import { Folder } from "../models/Folder";
import axios from 'axios';

export class FoldersRepository {
    private baseUrl: string = "https://localhost:5001/api/folders";

    public async GetFolders(): Promise<Folder[]> {
        var folders = await axios.get(this.baseUrl, {
            params: {
                limit: 100
            },
            headers: {
                headers: {
                    'Content-Type': 'application/json',
                    'Accept': '*/*'
                }
            }
        }).then(function (response) {
            return response.data.items.map((item: any) => new Folder(item.id, item.name));
        }).catch(function (error) {
            console.log(error);
        });
        return folders;
    }

    public async GetSubfolders(folderId: number): Promise<Folder[]> {
        var folders = await axios.get(this.baseUrl + `/${folderId.toString()}/subfolders`, {
            params: {
                limit: 100
            },
            headers: {
                headers: {
                    'Content-Type': 'application/json',
                    'Accept': '*/*'
                }
            }
        }).then(function (response) {
            return response.data.items.map((item: any) => new Folder(item.id, item.name, [], item.parentId));
        }).catch(function (error) {
            console.log(error);
        });
        return folders;
    }

    public async CreateFolder(folderName: string): Promise<Folder> {
        var createdFolder = await axios.post(this.baseUrl, {
            name: folderName
        }).then(function(response) {
            var item = response.data;
            return new Folder(item.id, item.name);
        }).catch(function (error) {
            console.log(error);
        });
        return createdFolder as Folder;
    }

    public async CreateSubfolder(folderName: string, parentId: number): Promise<Folder> {
        var createdFolder = await axios.post(this.baseUrl + `/${parentId.toString()}/subfolders`, {
            name: folderName
        }).then(function(response) {
            var item = response.data;
            return new Folder(item.id, item.name, [], item.parentId);
        }).catch(function (error) {
            console.log(error);
        });
        return createdFolder as Folder;
    }
}