import {AfterViewChecked, Component, Input, OnInit} from '@angular/core';
import {DiagnoseService} from "../../../services/diagnose.service";
import {Diagnose} from "../../../models/diagnose.model";
import {Location} from "@angular/common";
import {ActivatedRoute, Router} from "@angular/router";
import {Sort} from "@angular/material";
import {Differentialdiagnose} from "../../../models/differentialdiagnose.model";
import {DiagnoseSharedService} from "../diagnose-shared.service";

/**
 * @title Basic use of `<table mat-table>`
 */

  @Component({
  selector: 'max-differentialdiagnose-table',
  styleUrls: ['differentialdiagnose-table.component.scss'],
  templateUrl: 'differentialdiagnose-table.component.html',
})
export class DifferentialDiagnoseTableComponent implements OnInit{

  displayedColumns: string[] = ['icd', 'technicalTerm', 'synonym', 'prevalence'];
  differentialdiagnoses: Differentialdiagnose[];
  @Input() diagnose : Diagnose;

  constructor(
    private diagnoseSharedService: DiagnoseSharedService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.diagnoseSharedService.diagnose$
      .subscribe(diagnose =>{
        let df = diagnose.differentialdiagnoses.reverse();
        this.differentialdiagnoses = df;

      });
  }

  ngAfterContentInit(){
    this.differentialdiagnoses = this.diagnose.differentialdiagnoses;
  }

  sortData(sort: Sort) {
    const data = this.diagnose.differentialdiagnoses.slice();
    if (!sort.active || sort.direction === '') {
      this.differentialdiagnoses = data;
      return;
    }

    this.differentialdiagnoses = data.sort((a, b) => {
      const isAsc = sort.direction === 'asc';
      switch (sort.active) {
        case 'synonym': return compare(a.synonyms, b.synonyms, isAsc);
        case 'technicalTerm': return compare(a.technicalTerm, b.technicalTerm, isAsc);
        case 'icd': return compare(a.icds, b.icds, isAsc);
        case 'prevalence': return compare(a.prevalence, b.prevalence, isAsc);
        default: return 0;
      }
    });
  }

  routeDiagnose(row){
    console.log(row.diagnoseId);
    let id = row['diagnoseId'];

    console.log(id);

    this.router.navigate(['/diagnose/' + id]);
  }
}

function compare(a, b, isAsc) {
  if (a && b)
    return (a < b ? -1 : 1) * (isAsc ? 1 : -1);
  if (a)
    return 1;
  if (b)
    return -1;
}
