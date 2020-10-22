import {Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {MatAutocompleteSelectedEvent, MatChipInputEvent} from "@angular/material";
import {Tag} from "../../../models/tag.model";
import {TagService} from "../../../services/tag.service";
import {FormControl} from "@angular/forms";
import {COMMA, ENTER} from "@angular/cdk/keycodes";
import {Observable} from "rxjs/Observable";
import {map, startWith} from "rxjs/operators";

@Component({
  selector: 'max-tag-create',
  templateUrl: './tag-create.component.html',
  styleUrls: ['./tag-create.component.scss']
})
export class TagCreateComponent implements OnInit {

  chipListMessage: string = "Werte lassen sich durch ein Komma oder durch eine Entereingabe trennen";
  separatorKeysCodes: number[] = [ENTER, COMMA];
  tagCtrl = new FormControl();
  filteredTags: Observable<Tag[]> = new Observable<Tag[]>(); // Gehe durch alle gefilterten Tags
  public tags: Tag[] = []; // Diese Liste beinhaltet die Tags aus der Chip Liste
  allTags: Tag[] = []; // Diese List beinhaltet alle Tags

  @ViewChild('tagInput') tagInput: ElementRef;


  constructor(private tagService: TagService)
  {
    this.filteredTags = this.tagCtrl.valueChanges.pipe(
      startWith(null),
      map((name: string | null) => name ? this._filter(name) : this.allTags.slice()));
  }

  ngOnInit() {
    this.tagService.getTags().subscribe(tags => {
      this.allTags = tags as Tag[];
    });
  }

  private _filter(value: string): Tag[] {
    const filterValue = value.toLowerCase();
    return this.allTags.filter(tag => tag.name.toLowerCase().indexOf(filterValue) === 0);
  }

  //Bei Enterklicken wird ein Tag hinzugefügt
  addTag(event: MatChipInputEvent): void {
    const input = event.input;
    const value = event.value;

    if ((value || '').trim()) {
      let tag = this.allTags.find(tag => tag.name == value);
      if(tag){
        this.tags.push(tag);

        const index = this.allTags.indexOf(tag);
        if(index >= 0){
          this.allTags.splice(index, 1);
        }
      }else{
        let aTag = this.tags.find(tag => tag.name == value.trim());
        if(aTag){
          this.tagInput.nativeElement.value = '';
          this.tagCtrl.setValue(null);
        }else{
          this.tags.push({ name: value.trim() });
        }
      }
    }

    if (input) {
      input.value = '';
    }

    this.tagCtrl.setValue(null);
  }

  //Beim löschen eines Tags
  removeTag(tag: Tag): void {
    const index = this.tags.indexOf(tag);

    if(tag.tagId){
      this.allTags.push(tag);
    }

    if (index >= 0) {
      this.tags.splice(index, 1);
    }
  }

  // Autocomplete: Selektiere ein Tag
  // -> suche ein Tag in der Liste der AllTags
  selectedTag(event: MatAutocompleteSelectedEvent): void {
    let tag =  this.allTags.find(tag => tag.name == event.option.viewValue);

    if (tag) {
      this.tags.push(tag);

      const index = this.allTags.indexOf(tag);
      if (index >= 0) {
        this.allTags.splice(index, 1);
      }
    }
    this.tagInput.nativeElement.value = '';
    this.tagCtrl.setValue(null);
  }

}
