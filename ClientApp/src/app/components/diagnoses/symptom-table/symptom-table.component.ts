import {Component, Input, OnInit, ViewChild} from '@angular/core';
import {InputFileSystem} from "@ngtools/webpack/src/webpack";
import {Diagnose} from "../../../models/diagnose.model";
import {MatSort, MatTableDataSource, Sort} from "@angular/material";
import {DiagnoseSymptom} from "../../../models/diagnoseSymptom.model";
import {Router} from "@angular/router";

@Component({
  selector: 'max-symptom-table',
  templateUrl: './symptom-table.component.html',
  styleUrls: ['./symptom-table.component.scss']
})
export class SymptomTableComponent implements OnInit {

  displayedColumns: string[] = ['technicalTerm', 'synonym'];
  symptoms : DiagnoseSymptom[];

  @Input() diagnose : Diagnose;

  ngOnInit() {
  }

  constructor(
    private router: Router
  ){}

  ngAfterContentInit(){
    this.symptoms = this.diagnose.symptoms;
  }

  sortData(sort: Sort) {
    const data = this.diagnose.symptoms.slice();
    if (!sort.active || sort.direction === '') {
      this.symptoms = data;
      return;
    }

    this.symptoms = data.sort((a, b) => {
      const isAsc = sort.direction === 'asc';
      switch (sort.active) {
        case 'synonym': return compare(a.synonym, b.synonym, isAsc);
        case 'technicalTerm': return compare(a.technicalTerm, b.technicalTerm, isAsc);
        default: return 0;
      }
    });
  }

  routeDiagnose(row){
    console.log(row);
    let id = row['id'];
    this.router.navigate(['/symptom/'+ id]);
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
