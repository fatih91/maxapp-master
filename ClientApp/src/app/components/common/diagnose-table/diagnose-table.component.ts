import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {Differentialdiagnose} from "../../../models/differentialdiagnose.model";
import {MatTableDataSource} from "@angular/material";
import {SelectionModel} from "@angular/cdk/collections";
import {DiagnoseSymptom} from "../../../models/diagnoseSymptom.model";

@Component({
  selector: 'max-diagnose-table',
  templateUrl: './diagnose-table.component.html',
  styleUrls: ['./diagnose-table.component.scss']
})
export class DiagnoseTableComponent implements OnInit {

  @Input() diagnoseTableContent: MatTableDataSource<Differentialdiagnose>;
  @Input() differentialdiagnoseSelection = new SelectionModel<Differentialdiagnose>(true, []);

  @Output("selectedDiagnoses") selectedDiagnoses = new EventEmitter<number[]>();

  diagnoseDisplayedColumns: string[] = ['select', 'icd', 'technicalTerm', 'synonym', 'prevalence'];

  //TODO allg. Ressource für Diagnose-Model einführen, statt: Symptomdiagnose, Diff.-Diagnose..

  constructor() { }

  ngOnInit() {
  }

  applyFilter(filterValue: string, context : any) {
    context.filter = filterValue.trim().toLowerCase();
  }

  testToggle(row: any){
    this.differentialdiagnoseSelection.toggle(row);
    this.selectedRows();

  }

  masterToggle(whichTable: string) {


        this.isAllSelected() ? this.differentialdiagnoseSelection.clear() : this.diagnoseTableContent.data.forEach(row => this.differentialdiagnoseSelection.select(row));
        this.selectedRows();

  }


  isAllSelected() {
    let numSelected: number;
    let numRows: number;


        numSelected = this.differentialdiagnoseSelection.selected.length;
        numRows = this.diagnoseTableContent.data.length;



    return numSelected === numRows;
  }

  selectedRows() {
    let differentialdiagnosesIds = this.differentialdiagnoseSelection.selected.map(selected => {
      return selected.diagnoseId;
    });
    this.selectedDiagnoses.emit(differentialdiagnosesIds);
  }

}
