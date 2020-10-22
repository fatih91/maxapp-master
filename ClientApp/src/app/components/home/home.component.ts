import {Component, OnInit, TemplateRef, ViewChild, ViewEncapsulation} from '@angular/core';
import {Diagnose} from "../../models/diagnose.model";
import {Observable, Subject} from "rxjs/Rx";
import {DiagnoseService} from "../../services/diagnose.service";
import {debounceTime, distinctUntilChanged, switchMap} from "rxjs/internal/operators";

@Component({
    selector: 'home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.scss'],
    encapsulation: ViewEncapsulation.None
})
export class HomeComponent{
  //The dollar sign $ suffixed to a name is a soft convention to indicate that the variable is a stream.
  public searchDiagnoseTerms = new Subject<string>();
  public searchSymptomTerms = new Subject<string>();
  private diagnoseTerm = '';
  private symptomTerm = '';
  @ViewChild("searchBox") input;

  constructor(private diagnoseService: DiagnoseService){}

  search(term: string, diagnoseTabActive: boolean): void {
    if(term.length > 0){
      if(diagnoseTabActive){
        this.searchDiagnoseTerms.next(term);
        this.diagnoseTerm = term;
      }else{
        this.searchSymptomTerms.next(term);
        this.symptomTerm = term;
      }
    }
  }

  tabChanged(event:any){
    switch (event.index){
      case 0:{
        this.input.nativeElement.value = this.diagnoseTerm;
        break;
      }
      case 1:{
        this.input.nativeElement.value = this.symptomTerm;
        break;
      }
    }
  }

}
