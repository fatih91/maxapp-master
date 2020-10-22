import { Injectable } from '@angular/core';
import {Diagnose} from "../../models/diagnose.model";
import {Subject} from "rxjs/Rx";

@Injectable({
  providedIn: 'root'
})
export class DiagnoseSharedService {
  private diagnoseSource = new Subject<Diagnose>();

  diagnose$ = this.diagnoseSource.asObservable();

  setDiagnoseSource(diagnose: Diagnose){
    this.diagnoseSource.next(diagnose);
  }
}
