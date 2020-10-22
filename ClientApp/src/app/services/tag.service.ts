import {Inject, Injectable} from "@angular/core";

import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Diagnose} from "../models/diagnose.model";
import {Observable} from "rxjs/Observable";
import {SaveDiagnose} from "../models/saveDiagnose.model";
import {Filter} from "../models/filter.model";
import {Tag} from "../models/tag.model";
import {SaveTag} from "../models/saveTag.model";

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
    'Authorization': 'Bearer ' + localStorage.getItem("auth_token")
  })
};

@Injectable()
export class TagService {
    private readonly tagEndpoint = "/api/tags";
    baseUrl: string;

    constructor(private http: HttpClient){}

    getTags(filter?: Filter): Observable<Tag[]> {
        return this.http.get<Tag[]>(this.tagEndpoint);// + '?' + this.toQueryString(filter.searchTags));
    }

    getTag(id: number): Observable<Tag> {
        return this.http.get<Tag>(this.tagEndpoint + '/' + id)
    }

    createTag(tag: SaveTag): Observable<Tag> {
        return this.http.post<Tag>(this.tagEndpoint, tag);
    }

    createTags(tags: Tag[]): Observable<Tag[]>{
      return this.http.post<Tag[]>(this.tagEndpoint + '/createtags', tags);
    }

    deleteTag(id: number): Observable<{}> {
        const url = `${this.tagEndpoint}/${id}`;
        return this.http.delete(url);
    }

    updateTag(tag : Tag): Observable<Tag> {
        return this.http.put<Tag>(this.tagEndpoint, tag);
    }

    toQueryString(obj : string[]) {
        var parts = [];
        for (var property in obj) {
            var value = obj[property];
            if (value != null && value != undefined)
                parts.push(encodeURIComponent(property) + '=' + encodeURIComponent(value));
        }

        return parts.join('&');
    }
}
