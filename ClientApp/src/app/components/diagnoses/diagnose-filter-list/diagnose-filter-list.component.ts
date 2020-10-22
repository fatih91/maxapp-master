import {Component, Input, OnInit} from '@angular/core';
import {DiagnoseService} from "../../../services/diagnose.service";
import {Diagnose} from "../../../models/diagnose.model";
import {Observable} from "rxjs/Rx";
import {debounceTime, distinctUntilChanged, switchMap} from "rxjs/internal/operators";
import {Subject} from "rxjs/Subject";

@Component({
  selector: 'max-diagnose-filter-list',
  templateUrl: './diagnose-filter-list.component.html',
  styleUrls: ['./diagnose-filter-list.component.scss']
})
export class DiagnoseFilterListComponent implements OnInit {
  diagnoses$: Observable<Diagnose[]>;

  @Input()
  public searchTerm: Subject<string>;



  constructor(private diagnoseService: DiagnoseService) { }

  ngOnInit(): void {

      this.diagnoses$ = this.searchTerm.pipe(
        debounceTime(300),
        distinctUntilChanged(),
        switchMap((term: string) => this.diagnoseService.getDiagnoses(term)),
      );

      this.diagnoses$.subscribe(test => console.log(test));

  }

  /*getDiagnoses(): void {
    this.diagnoseService.getDiagnoses("")
      .subscribe(diagnoses => this.diagnoses = diagnoses as Diagnose[])
  }*/

}
