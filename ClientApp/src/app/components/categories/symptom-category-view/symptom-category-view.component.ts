import {Component, Input, OnChanges, OnInit} from "@angular/core";
import {CategoryService} from "../../../services/category.service";
import {Symptom} from "../../../models/symptom.model";
import {Subscription} from "../../../../../node_modules/rxjs/Rx";
import {ActivatedRoute} from "@angular/router";
import {TodoItemFlatNode, TodoItemNode} from "../symptom-categories-create/symptom-categories-create.component";
import {MatTreeFlatDataSource} from "@angular/material";

@Component({
  selector: 'max-symptom-category-view',
  templateUrl: './symptom-category-view.component.html',
  styleUrls: ['./symptom-category-view.component.scss']
})
export class SymptomCategoryViewComponent implements OnInit, OnChanges {
  @Input("id")
  nodeId: number;

  searchText: string;

  subscription: Subscription;
  symptoms: CategorySymptom[];

  constructor(private categoryService: CategoryService, private route: ActivatedRoute,) { }

  ngOnInit() {
    //this.getCategory();
  }

  ngOnChanges(){
    console.log("This Node Id: ",this.nodeId);
    this.getCategory();
  }

  getCategory(): void{
    this.categoryService.getCategory(this.nodeId)
      .subscribe(category =>{
        this.symptoms = category.symptoms;
        console.log(this.symptoms);
      });
  }
  oldSymptomList: CategorySymptom[];
  oldFilterValueLength: number;

  applyFilter(filterValue: string) {
    //Klone die alte Symptomliste
    this.oldSymptomList = Object.assign([], this.symptoms);
    this.oldFilterValueLength = this.symptoms.length;

    if(this.oldFilterValueLength >= filterValue.length){
      this.symptoms = this.oldSymptomList.filter( os => os.technicalTerm.trim().toLowerCase().includes(filterValue.trim().toLowerCase()));
    }else{
      this.symptoms = this.symptoms.filter(s =>
        s.technicalTerm
          .trim()
          .toLowerCase()
          .includes(filterValue.trim().toLowerCase()));
    }

  }

}
