import {Directive, Input} from '@angular/core';
import { NG_ASYNC_VALIDATORS, AsyncValidator, AbstractControl, ValidationErrors } from '@angular/forms';
import { Observable } from 'rxjs/Observable';
import { HttpClient} from '@angular/common/http';
import 'rxjs/Rx';
import {Symptom} from "../models/symptom.model";

@Directive({
  selector: '[maxapp-UniqueSymptomTechnicalTermValidator]',
  providers: [
    {
      provide: NG_ASYNC_VALIDATORS,
      useExisting: UniqueSymptomTechnicalTermValidator,
      multi: true
    }
  ]
})

export class UniqueSymptomTechnicalTermValidator implements AsyncValidator{
  @Input("edit")
  checkId: number;

  constructor(private http: HttpClient) { }

  validate(control: AbstractControl): Observable<ValidationErrors> {

    return this.http.get<Symptom[]>("/api/symptoms")
      .map(symptoms => symptoms.filter(symptom => symptom.technicalTerm === control.value))
      .map(symptoms => {
        if (symptoms.length > 0 && this.checkId == null){
          return ({ "technicalTermExists": true });
        }
      });
  }
}



