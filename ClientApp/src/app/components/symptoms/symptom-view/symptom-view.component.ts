import {AfterViewInit, Component, OnChanges, OnInit, SimpleChanges, ViewChild, ViewEncapsulation} from '@angular/core';
import {SymptomService} from "../../../services/symptom.service";
import {ActivatedRoute, Router} from "@angular/router";
import {Subscription} from "rxjs/Rx";
import {Symptom} from "../../../models/symptom.model";
import { Location } from '@angular/common';
import {Parentcategory} from "../../../models/parentcategory.model";
import {CategoryService} from "../../../services/category.service";
import {Category} from "../../../models/category.model";
import {FormControl, FormControlDirective} from "@angular/forms";
import {MatDialog, MatTab, MatTabGroup, MatTabHeaderPosition, MatTableDataSource, Sort} from "@angular/material";
import {ImageDialogComponent} from "../../common/image-dialog/image-dialog.component";
import {Differentialdiagnose} from "../../../models/differentialdiagnose.model";
import {SymptomDiagnose} from "../../../models/symptomDiagnose.model";
import {Observable} from "rxjs/Observable";

@Component({
  selector: 'max-symptom-view',
  templateUrl: './symptom-view.component.html',
  styleUrls: ['./symptom-view.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class SymptomViewComponent implements OnInit {
  symptom: Symptom;
  parentcategories: Parentcategory[] = [];
  category: Category;
  @ViewChild(MatTabGroup)
  private matTabGroupComponent: MatTabGroup;
  displayedColumns: string[] = ['icd', 'technicalTerm', 'synonym', 'prevalence'];
  diagnoses: MatTableDataSource<SymptomDiagnose>;

  constructor(
    private symptomService: SymptomService,
    private categoryService: CategoryService,
    private route: ActivatedRoute,
    private location: Location,
    private router: Router,
    private dialog: MatDialog
  ) { }

  ngOnInit() {
    this.getSymptom();


  }

  openImageDialog(): void{
    const dialogRef = this.dialog.open(ImageDialogComponent, {
      width: '250px',
      height: '250px',
     data: {images: ["hallo", "hallo"]}
    });
  }

  getSymptom(): void{
    this.route.params
      .flatMap(params => {
        const id = (params['id'] || '');
        return this.symptomService.getSymptom(id);
      })
      .flatMap(symptom => {
        this.symptom = symptom as Symptom;
        let symptomDiagnoses = this.symptom.diagnoses.map<SymptomDiagnose>(diagnose => {
          return {diagnoseId: diagnose.diagnoseId, icds: diagnose.icds, prevalence: diagnose.prevalence, synonyms: diagnose.synonyms, technicalTerm: diagnose.technicalTerm};
        });
        this.diagnoses = new MatTableDataSource<SymptomDiagnose>(symptomDiagnoses);

        if(symptom.category) {
          let c: Parentcategory = {parentId: symptom.category.categoryId, name: symptom.category.name};
          this.parentcategories.push(c);
          return this.categoryService.getParentCategories(symptom.category.categoryId);
        }else {
          return new Observable<Parentcategory[]>();
        }
      })
      .subscribe(parentcategories =>{
        this.parentcategories = parentcategories.filter(pc => pc.name != "root");
        this.parentcategories.reverse();
        this.matTabGroupComponent.selectedIndex = parentcategories.length + 1;
      });
  }



  getCategory(tabId: number): void{
    if (this.parentcategories != null){
      let parentcategorySize = this.parentcategories.length;
      if(tabId <= parentcategorySize && tabId >= 0) {
        let parentId;
        if (tabId < parentcategorySize){
          parentId = this.parentcategories[tabId].parentId;
          console.log(this.parentcategories[tabId].parentId);
        } else{
          parentId = this.symptom.category.categoryId;
        }
        this.categoryService.getCategory(parentId)
          .subscribe(category => {
            this.category = category;
            console.log(category)
          });
      }
    }

  }

  goBack(): void{
    this.location.back();
  }

  sortData(sort: Sort) {
    const data = this.symptom.diagnoses.slice();
    if (!sort.active || sort.direction === '') {
      this.symptom.diagnoses = data;
      return;
    }

    this.symptom.diagnoses = data.sort((a, b) => {
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
    let id = row['diagnoseId'];
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
