import {HttpClient} from "@angular/common/http";
import {SaveImage} from "../models/saveImage.model";
import {Injectable} from "@angular/core";
import {Observable} from "rxjs/Observable";
import {Image} from "../models/image.model";


@Injectable()
export class ImageService{

  constructor(private http: HttpClient){}

  upload(symptomId, image): Observable<Image>{
    var formData = new FormData();
    formData.append('file', image);
    return this.http.post<Image>(`/api/symptoms/${symptomId}/images`, formData);
  }

  getImages(symptomId): Observable<Image[]>{
    return this.http.get<Image[]>(`/api/symptoms/${symptomId}/images`);
  }

  updateImage(symptomId, fileName, saveImage: SaveImage){
    return this.http.put<Image>(`/api/symptoms/${symptomId}/images/${fileName}`, saveImage);
  }

  deleteImage(fileName: string, symptomId: number): Observable<string> {
    return this.http
      .delete(`/api/symptoms/${symptomId}/images/${fileName}`)
      .map(id => id as string);
  }

}
