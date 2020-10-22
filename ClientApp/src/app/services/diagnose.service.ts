import {Injectable} from "@angular/core";

import {HttpClient} from "@angular/common/http";
import {Diagnose} from "../models/diagnose.model";
import {Observable} from "rxjs/Observable";
import {SaveDiagnose} from "../models/saveDiagnose.model";
import {Filter} from "../models/filter.model";


@Injectable()
export class DiagnoseService {
    private readonly diagnoseEndpoint = "/api/diagnoses";
    baseUrl: string;

    constructor(private http: HttpClient){}

    getDiagnoses(term?: string): Observable<Diagnose[]>{//filter?: Filter) {
      //ToDo: schaue nach const searchParams = new URLSearchParams(); für Angular
      //cofngiJson = 'assets/config.json'; // in test

      if (!term){
        return this.http.get<Diagnose[]>(this.diagnoseEndpoint);
      }

      return this.http.get<Diagnose[]>(this.diagnoseEndpoint + '?q=' + term); // in production

      /*return this.http.get<Hero[]>(`${this.heroesUrl}/?name=${term}`).pipe(
        tap(_ => this.log(`found heroes matching "${term}"`)),
        catchError(this.handleError<Hero[]>('searchHeroes', []))
      );*/

      // /return this.http.get(this.diagnoseEndpoint); // + '?' + this.toQueryString(filter));
    }

    getDiagnose(id: number): Observable<Diagnose> {
        return this.http.get<Diagnose>(this.diagnoseEndpoint + '/' + id)
    }

    createDiagnose(diagnose: SaveDiagnose): Observable<Diagnose> {

      //this.http.post<Diagnose>(this.diagnoseEndpoint, diagnose).subscribe();
      return this.http.post<Diagnose>(this.diagnoseEndpoint, diagnose);
    }

    deleteDiagnose(diagnoseId: number): Observable<number> {
        return this.http
          .delete(`${this.diagnoseEndpoint}/${diagnoseId}`)
          .map(id => id as number);
    }

    updateDiagnose(diagnose : SaveDiagnose): Observable<Diagnose> {
        return this.http.put<Diagnose>(this.diagnoseEndpoint + '/' + diagnose.diagnoseId , diagnose);
    }

    checkTechnicalTermNotTaken(technicalTerm: string) {
      var result = this.http.get<Diagnose[]>(this.diagnoseEndpoint)
        .map(diagnoses => diagnoses.filter(diagnose => diagnose.technicalTerm === technicalTerm))
        .map(diagnoses => !diagnoses.length);

      console.log(result);
      return result;
    }


    toQueryString(filter: Filter) { //ToDo: Check: SearchTerm, SearchIcd und SearchTags

      var request = [];
        for (var property in filter.searchTags) {
            var value = filter.searchTags[property];
            if (value != null && value != undefined)
                request.push(encodeURIComponent(property) + '=' + encodeURIComponent(value));
        }

        if (filter.searchTerm!=null){
          request.push("searchTerm" + "=" + filter.searchTerm)
        }

        if (filter.icd != null){
          request.push("Icd" + "=" + "filter.Icd")
        }

        return request.join('&');
    }

}
