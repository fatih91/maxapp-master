import { async, ComponentFixture, TestBed } from "@angular/core/testing";

import { SymptomCategoryViewComponent } from "./symptom-category-view.component";

describe('SymptomCategoryViewComponent', () => {
  let component: SymptomCategoryViewComponent;
  let fixture: ComponentFixture<SymptomCategoryViewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SymptomCategoryViewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SymptomCategoryViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
