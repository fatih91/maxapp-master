import {Component, ElementRef, OnInit, Output, ViewChild} from '@angular/core';
import {FormControl, Validators, FormGroupDirective, NgForm, FormBuilder, AbstractControl, FormGroup} from "@angular/forms";
import {Diagnose} from "../../../models/diagnose.model";
import {DiagnoseService} from "../../../services/diagnose.service";
import {SaveDiagnose} from "../../../models/saveDiagnose.model";
import {
  MatAutocompleteSelectedEvent,
  MatChipInputEvent,
  MatInputModule,
  MatTableDataSource,
  ErrorStateMatcher,
  MatDialog
} from "@angular/material";
import {COMMA, ENTER} from "@angular/cdk/keycodes";
import {KeyValuePair} from "../../../models/keyValuePair.model";
import {Checklist} from "../../../models/checklist.model";
import {DiagnoseSymptom} from "../../../models/diagnoseSymptom.model";
import {SelectionModel} from "@angular/cdk/collections";
import {SymptomService} from "../../../services/symptom.service";
import {Symptom} from "../../../models/symptom.model";
import {TagService} from "../../../services/tag.service";
import {Tag} from "../../../models/tag.model";
import {Observable} from "rxjs/Observable";
import {map, startWith} from "rxjs/operators";
import {Prognose} from "../../../models/prognose.model";
import {Differentialdiagnose} from "../../../models/differentialdiagnose.model";
import {PopupImageComponent} from "../../common/popup-image/popup-image.component";
import {ImageDialogComponent} from "../../common/image-dialog/image-dialog.component";
import {Location} from '@angular/common';
import {ActivatedRoute, Router} from "@angular/router";
import {TagCreateComponent} from "../../common/tag-create/tag-create.component";

@Component({
  selector: 'max-diagnose-create',
  templateUrl: './diagnose-create.component.html',
  styleUrls: ['./diagnose-create.component.scss']
})
export class DiagnoseCreateComponent implements OnInit {

  @ViewChild('chipIcdList') chipIcdList;

  chipListMessage: string = "Werte lassen sich durch ein Komma oder durch eine Entereingabe trennen";
  diagnose: SaveDiagnose = this.createInitialDiagnose();
  checkId: number;
  synonyms: KeyValuePair[] = [];
  icds: KeyValuePair[] = [];
  diagnostics: KeyValuePair[] = [];
  prognoses: KeyValuePair[] = [];
  therapies: KeyValuePair[] = [];
  checklist: Checklist[] = [];
  differentialdiagnoses: number[] = [];
  symptomDisplayedColumns: string[] = ['select', 'technicalTerm', 'synonym'];
  saveSymptoms: DiagnoseSymptom[];
  symptomTableContent: MatTableDataSource<DiagnoseSymptom>;
  differentialdiagnoseTableContent: MatTableDataSource<Differentialdiagnose>;
  symptomSelection: SelectionModel<DiagnoseSymptom> = new SelectionModel<DiagnoseSymptom>(true, []);
  differentialdiagnoseSelection = new SelectionModel<Differentialdiagnose>(true, []);
  symptoms: Symptom[];
  visible = true;
  selectable = true;
  removable = true;
  addOnBlur = true;
  readonly separatorKeysCodes: number[] = [ENTER, COMMA];
  imageFileName: string;
  @ViewChild('tagCreate') tagComponent: TagCreateComponent;
  referencedTags: number[] = [];
  //differentialdiagnoses: Differentialdiagnose[];

  constructor(
    private route: ActivatedRoute,
    private location: Location,
    private router: Router,
    private dialog: MatDialog,
    private diagnoseService: DiagnoseService,
    private symptomService: SymptomService,
    private tagService: TagService) {}


  createInitialDiagnose(): SaveDiagnose{
    return {
      symptoms: [],
      tags: [],
      checklists: [],
      prognoses: [],
      differentialdiagnoses: [],
      icds: [],
      diagnostics: [],
      synonyms: [],
      therapies: []
    } as SaveDiagnose
  }

  ngOnInit() {
    this.route.params
      .subscribe(params => {
        const id = (params['id'] || '');
        if(id){
          this.checkId = id;
          this.diagnoseService.getDiagnose(id)
            .map(diagnose => {
              return {
                diagnoseId: diagnose.diagnoseId,
                technicalTerm: diagnose.technicalTerm,
                synonyms: diagnose.synonyms,
                icds: diagnose.icds,
                definition: diagnose.definition,
                inheritance: diagnose.inheritance,
                reason: diagnose.reason,
                diagnostics: diagnose.diagnostics,
                ageTime: diagnose.ageTime,
                season: diagnose.season,
                prevalence: diagnose.prevalence,
                checklists: diagnose.checklists,
                prognoses: diagnose.prognoses,
                therapies: diagnose.therapies,
                originalTags: diagnose.tags,//.map(t => t.tagId),
                fileName: diagnose.image ? diagnose.image.fileName : '',
                symptoms: diagnose.symptoms.map(s => s.id),
                differentialdiagnoses: diagnose.differentialdiagnoses.map(df => df.diagnoseId)

              }
            })
            .flatMap(diagnose =>
            {
              this.diagnose = diagnose;
              this.synonyms = diagnose.synonyms;
              this.icds = diagnose.icds;
              this.diagnostics = diagnose.diagnostics;
              this.tagComponent.tags = diagnose.originalTags;
              this.prognoses = diagnose.prognoses;
              this.therapies = diagnose.therapies;
              this.checklist = diagnose.checklists;
              this.imageFileName = diagnose.fileName;
              this.differentialdiagnoses = diagnose.differentialdiagnoses;
              return this.symptomService.getSymptoms();
            }).flatMap(symptoms =>
            {
              this.symptoms = symptoms;

              let diagnoseSymptom = this.symptoms.map(symptom => {
                return {id: symptom.symptomId, technicalTerm: symptom.technicalTerm, synonyms: symptom.synonyms};
              });

              this.symptomTableContent = new MatTableDataSource<DiagnoseSymptom>(diagnoseSymptom);

              // Falls es um das editieren von der Diagnose geht werden hier die bereits markierten Symptome gefiltert.
              // Und als Initialisierungdaten für den SelectionModel reingepackt, hierbei ist zu betrachten, dass
              // die Referenzierung eine besondere Rolle spielt. Das SelectionModel enthält ein SET, der eindeutige Referenzobjekte
              // erwartet
              if(this.diagnose.symptoms.length > 0){
                let checkedSymptoms = diagnoseSymptom.filter(s => this.diagnose.symptoms.some(id => id == s.id));
                this.symptomSelection = new SelectionModel<DiagnoseSymptom>(true, checkedSymptoms);
              }
              return this.diagnoseService.getDiagnoses();
            }).subscribe(diagnoses =>
            {
                let differentialdiagnoses = diagnoses.filter(d => this.diagnose.diagnoseId != d.diagnoseId).map<Differentialdiagnose>(diagnose => {

                return {diagnoseId: diagnose.diagnoseId, icd: diagnose.icds, prevalence: diagnose.prevalence, synonym: diagnose.synonyms, technicalTerm: diagnose.technicalTerm} as Differentialdiagnose;

              });
              this.differentialdiagnoseTableContent = new MatTableDataSource<Differentialdiagnose>(differentialdiagnoses);

              if(this.diagnose.differentialdiagnoses.length > 0){
                let checkedDiagnoses = differentialdiagnoses.filter(d => this.diagnose.differentialdiagnoses.some(id => id == d.diagnoseId));
                this.differentialdiagnoseSelection = new SelectionModel<Differentialdiagnose>(true, checkedDiagnoses);
              }

            });
        }else{
          this.diagnose = this.createInitialDiagnose();

          this.symptomService.getSymptoms().subscribe(symptoms => {
            this.symptoms = symptoms;

            let diagnoseSymptom = this.symptoms.map(symptom => {
              return {id: symptom.symptomId, technicalTerm: symptom.technicalTerm, synonyms: symptom.synonyms};
            });

            this.symptomTableContent = new MatTableDataSource<DiagnoseSymptom>(diagnoseSymptom);
          });

          this.diagnoseService.getDiagnoses().subscribe(diagnoses => {

            let differentialdiagnoses = diagnoses.map<Differentialdiagnose>(diagnose => {

              return {diagnoseId: diagnose.diagnoseId, icd: diagnose.icds, prevalence: diagnose.prevalence, synonym: diagnose.synonyms, technicalTerm: diagnose.technicalTerm} as Differentialdiagnose;

            });
            this.differentialdiagnoseTableContent = new MatTableDataSource<Differentialdiagnose>(differentialdiagnoses)
          });
        }
      });
  }

  //todo typeguard anwenden
  selectedDiagnoses(event:any){
    this.differentialdiagnoses = event;
  }

  saveDiagnose(value: any) {
    this.diagnose = value;
    this.diagnose.fileName = this.imageFileName;
    this.diagnose.synonyms = this.synonyms;
    if(!this.chipIcdList.errorState)
      this.diagnose.icds = this.icds;
    this.diagnose.diagnostics = this.diagnostics;
    this.diagnose.prognoses = this.prognoses;
    this.diagnose.therapies = this.therapies;
    this.diagnose.checklists = this.checklist;
    this.diagnose.differentialdiagnoses = this.differentialdiagnoses;
    this.selectedSymptomRows();
    this.selectedTags();
    this.diagnose.tags = this.referencedTags;

     //ToDo: Subscribe später, benutze eine Observable Objekt

    if(this.checkId){
      this.diagnose.diagnoseId = this.checkId;
      this.addNewTags().flatMap(tags =>
      {
        let ids = tags.map(tag => tag.tagId);
        if(ids)
          this.diagnose.tags.push(...ids);
        return this.diagnoseService.updateDiagnose(this.diagnose)
      }).subscribe(diagnose => {
        const relativeUrl = this.router.url.includes('edit') ? '../..' : '..';
        this.router.navigate([relativeUrl], {relativeTo: this.route});
      });
    }else{
      this.addNewTags().flatMap(tags =>{
        let ids = tags.map(tag => tag.tagId);
        if(ids)
          this.diagnose.tags.push(...ids);
        return this.diagnoseService.createDiagnose(this.diagnose)
      }).subscribe(diagnose => {
        const relativeUrl = this.router.url.includes('edit') ? '../..' : '..';
        this.router.navigate([relativeUrl], {relativeTo: this.route});

        console.log("yo,", this.diagnose);

      });
    }


  }

  //SaveDiagnose führe SelectedTags Methode aus, um auf bestehende Tags zu referenzieren
  selectedTags() {
    let existingTags = this.tagComponent.tags
      .filter(tag => tag.tagId != null && !this.referencedTags.includes(tag.tagId))
      .map(tag => tag.tagId);

    if(existingTags)
      this.referencedTags.push(...existingTags);

  }

  // Führe diese Methode bei Save Diagnose aus
  addNewTags():Observable<Tag[]> {
    let newTags = this.tagComponent.tags
      .filter(tag => tag.tagId == null);

    return this.tagService.createTags(newTags);
  }

  openDialog(): void{
    let symptomIds: number[] = this.symptomSelection.selected.map(selected => {
      return selected.id;
    });

    const dialogRef = this.dialog.open(PopupImageComponent, {
      //width: '75%',
      //height: '100%',
      data: [symptomIds]
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log(result);
      if(result)
        this.imageFileName = result.fileName;
    });
  }

  //Diagnose und Symptom Tabelle
  isAllSelected(whichTable: string) {
    let numSelected: number;
    let numRows: number;
    switch(whichTable){
      case "symptom":{
        numSelected = this.symptomSelection.selected.length;
        numRows = this.symptomTableContent.data.length;
        break;

      }
    }
    return numSelected === numRows;
  }

  masterToggle(whichTable: string) {
    switch(whichTable){
      case "symptom":{
        this.isAllSelected(whichTable) ? this.symptomSelection.clear() : this.symptomTableContent.data.forEach(row => this.symptomSelection.select(row));
        break;
      }
    }
  }

  isCheckboxSelected(): boolean{
    return this.symptomSelection.selected.length > 0;
  }

// Beim Speichern werden die selektierten Checkboxen in einer List aus Zahlen gepackt, um auf diese referenzieren zu könnnen
  selectedSymptomRows() {
    let symptomIds = this.symptomSelection.selected.map(selected => {
      return selected.id;

    });
    this.diagnose.symptoms = symptomIds;

  }


  add(event: MatChipInputEvent, context: string): void {
    const input = event.input;
    const value = event.value;
    console.log("MATCHIPEVENT", event);

    if ((value || '').trim()) {
      switch (context) {

        case "synonyms": {
          this.synonyms.push({ value: value.trim() });
          break;
        }
        case "icds": {
          this.icds.push({ value: value.trim() });
          this.icdListValidate();
          break;
        }
        case "therapies": {
          this.therapies.push({ value: value.trim() });
          break;
        }
        case "prognoses": {
          this.prognoses.push({ value: value.trim() });
          break;
        }
        case "diagnostics": {
          this.diagnostics.push({ value: value.trim() });
          break;
        }
      }
    }

    // Reset the input value
    if (input) {
      input.value = '';
    }
  }


  addPrognose(){
    const isPrognoseEmpty = this.prognoses.some(prognose => prognose.value == '');

    if(!isPrognoseEmpty){
      this.prognoses.push({ value: '' });
    }
    return false;
  }

  testData(prognose, index) {
    console.log(prognose, index);
    console.log(this.prognoses);
  }

  addTherapy() {
    const isTherapyEmpty = this.therapies.some(therapy => therapy.value == '');

    if(!isTherapyEmpty){
      this.therapies.push({ value: '' });
    }
    return false;
  }

  addChecklist() {
    const isChecklistEmpty = this.checklist.some(checklist => checklist.checkup == '' && checklist.reason == '');

    if(!isChecklistEmpty){
      this.checklist.push({ checkup: '', reason: '' });
    }
    return false;
  }

  remove(value: any, context: string) {

    let list;

    switch (context) {

      case "synonyms": {
        list = this.synonyms;
        break;
      }
      case "icds": {
        list = this.icds;
        break;
      }
      case "therapies": {
        list = this.therapies;
        break;
      }
      case "prognoses": {
        list = this.prognoses;
        break;
      }
      case "diagnostics": {
        list = this.diagnostics;
        break;
      }
      case "checklist": {
        list = this.checklist;
        break;
      }
    }
    const index = list.indexOf(value);
    console.log("Liste vor dem löschen ohne dummy:", list);
    list[index] = "dummy";

    if (index >= 0) {
      console.log("Liste vor dem löschen mit dummy:", list);
      list.splice(index, 1);
      console.log("Liste nach dem löschen:", list);
    }
    this.icdListValidate();
    return false; //Die Rückgabe des Wertes false verhindert, dass ein Klick auf den jeweiligen Button das Formular absendet.
  }

  applyFilter(filterValue: string, context : any) {
    context.filter = filterValue.trim().toLowerCase();
  }

  static isDiagnose(context: any){
    return context.some(c =>{
      return 'diagnoseId' in c
    });
  }

  static isSymptom(context: any){
    let result = context.some(c =>{
      return 'symptomId' in c
    });
  }

  icdListValidate(){
    var result;

    const re = new RegExp("([A-TV-Z][0-9][A-Z0-9](\\.?[A-Z0-9]{0,4})?)");

    result = this.icds.every(icd =>
     re.test(icd.value)
    );
    if(result)
      this.chipIcdList.errorState = false;
    else {
      this.chipIcdList.errorState = true;
    }
  }

}
