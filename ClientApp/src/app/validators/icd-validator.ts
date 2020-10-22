import {Directive, Input} from '@angular/core';
import {NG_ASYNC_VALIDATORS, AsyncValidator, AbstractControl, ValidationErrors, NG_VALIDATORS} from '@angular/forms';
import { Observable } from 'rxjs/Observable';
import {HttpClient, HttpErrorResponse, HttpHeaders} from '@angular/common/http';
import 'rxjs/Rx';
import { DiagnoseService } from '../services/diagnose.service';
import { Diagnose } from '../models/diagnose.model';

@Directive({
  selector: '[max-icd-validator]',
  providers: [
    {
      provide: NG_VALIDATORS,
      useExisting: IcdValidator,
      multi: true
    }
  ]
})


export class IcdValidator{

  validate(control: AbstractControl): {[key: string]: any}  {
    const re = new RegExp("([A-TV-Z][0-9][A-Z0-9](\\.?[A-Z0-9]{0,4})?)");

    if(!control.value || control.value === '' || re.test(control.value)){
      return null;
    }else{
      return ({"invalidICD": true});
    }
  }
}



