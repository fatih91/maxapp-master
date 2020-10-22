import {Component, Input, OnInit} from '@angular/core';
import {SymptomService} from "../../../services/symptom.service";
import {Symptom} from "../../../models/symptom.model";
import {Observable, Subject} from "rxjs/Rx";
import {debounceTime, distinctUntilChanged, switchMap} from "rxjs/internal/operators";

@Component({
  selector: 'max-symptom-filter-list',
  templateUrl: './symptom-filter-list.component.html',
  styleUrls: ['./symptom-filter-list.component.scss']
})
export class SymptomFilterListComponent implements OnInit {
  symptoms$:Observable<Symptom[]>;

  @Input()
  public searchTerm: Subject<string>;


  constructor(private symptomService: SymptomService) { }

  ngOnInit() {

      this.symptoms$ = this.searchTerm.pipe(
        debounceTime(300),
        distinctUntilChanged(),
        switchMap((term:string) => this.symptomService.getSymptoms(term))
      );

  }

}
