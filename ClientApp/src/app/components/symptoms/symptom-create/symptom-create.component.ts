import {Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {SaveSymptom} from "../../../models/saveSymptom.model";
import {SymptomService} from "../../../services/symptom.service";
import {SelectionModel} from "@angular/cdk/collections";
import {KeyValuePair} from "../../../models/keyValuePair.model";
import {MatChipInputEvent, MatTableDataSource} from "@angular/material";
import {COMMA, ENTER} from "@angular/cdk/keycodes";
import {DiagnoseService} from "../../../services/diagnose.service";
import {SymptomDiagnose} from "../../../models/symptomDiagnose.model";
import {Diagnose} from "../../../models/diagnose.model";
import {ImageService} from "../../../services/image.service";
import {LocalImage} from "../../../models/local-image.model";
import {SaveImage} from "../../../models/saveImage.model";
import {Location} from "@angular/common";
import {Symptom} from "../../../models/symptom.model";
import {ActivatedRoute, Router} from "@angular/router";
import {TagCreateComponent} from "../../common/tag-create/tag-create.component";
import {TagService} from "../../../services/tag.service";
import {Observable} from "rxjs/Observable";
import {Tag} from "../../../models/tag.model";

@Component({
  selector: 'max-symptom-create',
  templateUrl: './symptom-create.component.html',
  styleUrls: ['./symptom-create.component.scss']
})
export class SymptomCreateComponent implements OnInit {
  symptom: SaveSymptom = this.createInitialSymptom();
  symptomId: number;
  synonyms: KeyValuePair[] = [];
  diagnoses: Diagnose[];
  localFiles: LocalImage[] = [];
  urls: any[] = [];
  genders: any[] = [
    {value: '0', viewValue: 'Männlich'},
    {value: '1', viewValue: 'Weiblich'}
  ];
  displayedColumns: string[] = ['select', 'technicalTerm', 'synonym'];
  diagnoseTableContent: MatTableDataSource<SymptomDiagnose>;
  diagnoseSelection = new SelectionModel<SymptomDiagnose>(true, []);
  @ViewChild('fileInput') fileInput: ElementRef;
  images: any[] = [];
  categoryId: number;
  checkId: number;
  @ViewChild('tagCreate') tagComponent: TagCreateComponent;
  referencedTags: number[] = [];

  visible = true;
  selectable = true;
  removable = true;
  addOnBlur = true;
  readonly separatorKeysCodes: number[] = [ENTER, COMMA];

  constructor(
    private route: ActivatedRoute,
    private location: Location,
    private router: Router,
    private diagnoseService: DiagnoseService,
    private symptomService: SymptomService,
    private imageService: ImageService,
    private tagService: TagService) {}



  ngOnInit() {
    this.route.params
      .subscribe(params =>{
        const id = (params['id'] || '');
        if(id){
          this.checkId = id;
          this.symptomService.getSymptom(id)
            .map(symptom => this.symptomToSaveSymptom(symptom))
            .flatMap(symptom => {
              this.symptom = symptom;
              this.categoryId = symptom.categoryId;
              this.synonyms = symptom.synonyms;
              this.tagComponent.tags = symptom.originalTags;
              this.images = symptom.originalImages;
              this.localFiles = symptom.originalImages.map(image => {
                      let test=  {age: image.age, gender: image.gender.toString(), imageDescription: image.imageDescription, fileName: image.fileName} as LocalImage;
                      this.images.push(test);
                      this.urls.push("/uploads/" + image.fileName);
                      return test;
              });

              return this.diagnoseService.getDiagnoses();
            }).subscribe(diagnoses =>
            {
              this.diagnoses = diagnoses;
              let symptomDiagnose = this.diagnoses.map(diagnose => {
                let rObj: SymptomDiagnose = { diagnoseId: diagnose.diagnoseId, technicalTerm: diagnose.technicalTerm, synonyms: diagnose.synonyms };
                return rObj;
              });

              this.diagnoseTableContent = new MatTableDataSource<SymptomDiagnose>(symptomDiagnose);

              if(this.symptom.diagnoses.length > 0){
                let checkedDiagnoses = symptomDiagnose.filter(d => this.symptom.diagnoses.some(id => id == d.diagnoseId));
                this.diagnoseSelection = new SelectionModel<SymptomDiagnose>(true, checkedDiagnoses);
              }
            });
        }else{
          this.symptom = this.createInitialSymptom();

          this.diagnoseService.getDiagnoses().subscribe(diagnoses => {
            this.diagnoses = diagnoses;
            let symptomDiagnose = this.diagnoses.map(diagnose => {
              let rObj: SymptomDiagnose = { diagnoseId: diagnose.diagnoseId, technicalTerm: diagnose.technicalTerm, synonyms: diagnose.synonyms };
              return rObj;
            });
            this.diagnoseTableContent = new MatTableDataSource<SymptomDiagnose>(symptomDiagnose);
          });
        }
      });
  }

  symptomToSaveSymptom(symptom: Symptom){
    return {
      symptomId: symptom.symptomId,
      technicalTerm: symptom.technicalTerm,
      synonyms: symptom.synonyms,
      definition: symptom.definition,
      diagnoses: symptom.diagnoses.map(diagnose => diagnose.diagnoseId),
      categoryId: symptom.category.categoryId,
      originalTags: symptom.tags,
      originalImages: symptom.images
    }
  }

  createInitialSymptom(): SaveSymptom{
    return {
      synonyms: [],
      diagnoses: [],
      tags: []
    }
  }

  deleteImageFromLocalFiles(index: number){
    console.log("INDEX", index);
    console.log("urls", this.urls);
    console.log("LOCALFILEINDEX", this.localFiles[index]);
    let imagetoDelete = this.localFiles[index];
    if(imagetoDelete.fileName && this.checkId){
      console.log("TESTCHECKID", this.checkId);
      this.imageService.deleteImage(imagetoDelete.fileName, this.checkId).subscribe();
    }
    this.urls.splice(index, 1);
    this.localFiles.splice(index, 1);
    this.fileInput.nativeElement.value = '';
  }

  selectedCategory(event:any){
      this.categoryId = event;
  }

  selectedDiagnoses(event:any){
    this.diagnoses = event;
  }

 // A1.1 -> A1, A1.1, A1.2, ...... -> put A1.1 ->

 /*imageDescription: string,
    age: number,
    gender: number*/

 uploadLocalImage(){


   var nativeElement: HTMLInputElement = this.fileInput.nativeElement;
   var file = nativeElement.files[0];
   //file.webkitRelativePath
  // this.localFiles.push(nativeElement.files[0]);
   this.readUrl(event);
 }

  readUrl(event:any) {

    if (event.target.files && event.target.files[0]) {
      this.localFiles.push({file: event.target.files[0]} as LocalImage);
      var reader = new FileReader();
      reader.onload = (event: ProgressEvent) => {
        this.urls.push((<FileReader>event.target).result);
      };
      reader.readAsDataURL(event.target.files[0]);
    }
  }

  isAllSelected(){
    const numSelected = this.diagnoseSelection.selected.length;
    const numRows = this.diagnoseTableContent.data.length;
    return numSelected === numRows;
  }

  masterToggle(){
    this.isAllSelected() ? this.diagnoseSelection.clear() : this.diagnoseTableContent.data.forEach(row => this.diagnoseSelection.select(row));
  }

  selectedRows(){
    let ids = this.diagnoseSelection.selected.map(selected => {
      let id = selected.diagnoseId;
      return id;
    });
    this.symptom.diagnoses  = ids;
  }

  add(event: MatChipInputEvent, context: string): void {
    const input = event.input;
    const value = event.value;

    if ((value || '').trim()) {
      switch (context) {
        case "synonyms": {
          this.synonyms.push({value: value.trim()});
          break;
        }
      }
    }

    // Reset the input value
    if (input) {
      input.value = '';
    }
  }

  remove(value: any, context: string): void {

    let list;


    switch (context) {

      case "synonyms": {
        list = this.synonyms;
        break;
      }
    }
    const index = list.indexOf(value);

    if (index >= 0) {
      list.splice(index, 1);
    }
  }

  saveSymptom(value: any) {
    this.symptom = value;
    this.symptom.synonyms = this.synonyms;
    this.selectedRows();
    this.symptom.categoryId = this.categoryId;
    this.selectedTags();
    this.symptom.tags = this.referencedTags;


    if(this.checkId){
      this.symptom.symptomId = this.checkId;
      this.addNewTags().flatMap(tags => {
        let ids = tags.map(tag => tag.tagId);
        if(ids)
          this.symptom.tags.push(...ids);
          this.uploadImage(this.checkId);
        return this.symptomService.updateSymptom(this.symptom)
      }).subscribe(symptom => {
        const relativeUrl = this.router.url.includes('edit') ? '../..' : '..';
        this.router.navigate([relativeUrl], {relativeTo: this.route});
      });
    }else{
      this.addNewTags().flatMap(tags => {
        let ids = tags.map(tag => tag.tagId);
        if(ids)
          this.symptom.tags.push(...ids);
        return this.symptomService.createSymptom(this.symptom);
      }).subscribe(symptom =>{
        this.symptomId = symptom.symptomId;
        this.uploadImage(symptom.symptomId);
        const relativeUrl = this.router.url.includes('edit') ? '../..' : '..';
        this.router.navigate([relativeUrl], {relativeTo: this.route});
      });
    }
  }

  //SaveDiagnose führe SelectedTags Methode aus, um diese zu referenzieren
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

  uploadImage(symptomId: number){
    // var nativeElement: HTMLInputElement = this.fileInput.nativeElement;
    // var file = nativeElement.files[0];

    this.localFiles.forEach(localImage => {

      if(localImage.file != null){
        this.imageService.upload(symptomId, localImage.file)
          .subscribe(image => {

            this.images.push(image);
            this.updateImage(image.fileName, localImage, symptomId);
          });
      }
      else{
        this.updateImage(localImage.fileName, localImage, symptomId)
      }
    });




    //nativeElement.value = '';
    /* this.imageService.upload(this.symptomId, file)
       .subscribe(image => {
         this.images.push(image);
       });*/
  }

  updateImage(fileName:string, localImage: LocalImage, symptomId: number){
    let lI: SaveImage = {gender: +localImage.gender, age: localImage.age, imageDescription: localImage.imageDescription};
    this.imageService.updateImage(symptomId,fileName, lI ).subscribe(ui => {
    });
  }

  applyFilter(filterValue: string) {
    this.diagnoseTableContent.filter = filterValue.trim().toLowerCase();
  }
}
