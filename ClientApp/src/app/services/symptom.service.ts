import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs/Observable";
import {Filter} from "../models/filter.model";
import {Symptom} from "../models/symptom.model";
import {SaveSymptom} from "../models/saveSymptom.model";

@Injectable()
export class SymptomService {
    private readonly symptomEndpoint = "api/symptoms";
    baseUrl: string;

    constructor(private http: HttpClient){}

    getSymptoms(filter?: string): Observable<Symptom[]> {
      if(!filter){
        return this.http.get<Symptom[]>(this.symptomEndpoint);
      }
      return this.http.get<Symptom[]>(this.symptomEndpoint + '?q=' + filter); //this.toQueryString(filter.searchTags));
    }

    getSymptom(id: number): Observable<Symptom> {
        return this.http.get<Symptom>(this.symptomEndpoint + '/' + id);
    }

    createSymptom(symptom: SaveSymptom): Observable<Symptom> {
        return this.http.post<Symptom>(this.symptomEndpoint, symptom);
    }

    deleteSymptom(symptomId: number): Observable<number> {
        return this.http
          .delete(this.symptomEndpoint + '/' + symptomId)
          .map(id => id as number);
    }

    updateSymptom(symptom : SaveSymptom): Observable<Symptom> {
        return this.http.put<Symptom>(this.symptomEndpoint + '/' + symptom.symptomId, symptom);
    }

  toQueryString(filter: Filter) { //ToDo: Check: SearchTerm, SearchTags

    var request = [];
    for (var property in filter.searchTags) {
      var value = filter.searchTags[property];
      if (value != null && value != undefined)
        request.push(encodeURIComponent(property) + '=' + encodeURIComponent(value));
    }

    if (filter.searchTerm!=null){
      request.push("searchTerm" + "=" + filter.searchTerm)
    }

    return request.join('&');
  }

}
