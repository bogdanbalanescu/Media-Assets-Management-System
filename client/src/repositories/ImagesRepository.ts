import { Image } from "../models/Image";
import axios from 'axios';

export class ImagesRepository {
    private baseUrl: string = "https://localhost:5001/api/images";

    public async GetImagesForFolder(folderId: number): Promise<Image[]> {
        var images = await axios.get(this.baseUrl, {
            params: {
                limit: 100,
                folderId: folderId
            },
            headers: {
                headers: {
                    'Content-Type': 'application/json',
                    'Accept': '*/*'
                }
            }
        }).then(function (response) {
            return response.data.items.map((item: any) => new Image(item.id, item.name, item.folderId));
        }).catch(function (error) {
            console.log(error);
        });
        return images;
    }

    public async CreateImage(image: any, folderId: number): Promise<Image> {
        const formData = new FormData();
        formData.append('imageFormFile', image);
        formData.append('folderId', folderId.toString());

        var createdImage = await axios.post(this.baseUrl, formData, {
            headers: {
                'content-type': 'multipart/form-data'
            }
        }).then(function(response) {
            var item = response.data;
            return new Image(item.id, item.name, item.folderId);
        }).catch(function (error) {
            console.log(error);
        });
        return createdImage as Image;
    }
}