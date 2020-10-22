import {Directive, Input} from '@angular/core';
import { NG_ASYNC_VALIDATORS, AsyncValidator, AbstractControl, ValidationErrors } from '@angular/forms';
import { Observable } from 'rxjs/Observable';
import {HttpClient, HttpErrorResponse, HttpHeaders} from '@angular/common/http';
import 'rxjs/Rx';
import { DiagnoseService } from '../services/diagnose.service';
import { Diagnose } from '../models/diagnose.model';

@Directive({
  selector: '[maxapp-UniqueDiagnoseTechnicalTermValidator]',
  providers: [
    {
      provide: NG_ASYNC_VALIDATORS,
      useExisting: UniqueDiagnoseTechnicalTermValidator,
      multi: true
    }
  ]
})


export class UniqueDiagnoseTechnicalTermValidator implements AsyncValidator{
  @Input("edit")
  checkId:number;

  constructor(private http: HttpClient) { }

  validate(control: AbstractControl): Observable<ValidationErrors> {

    return this.http.get<Diagnose[]>("/api/diagnoses")
      .map(diagnoses => diagnoses.filter(diagnose => {
       // console.log("Test: ", control.value, diagnose.technicalTerm);
       return diagnose.technicalTerm === control.value;
      }))
      .map(diagnoses => {
        if (diagnoses.length > 0 && this.checkId == null){
          console.log("Diagnose",diagnoses);
          return ({ "technicalTermExists": true });
        }

      } );
  }
}



