import { async, ComponentFixture, TestBed } from "@angular/core/testing";

import { SymptomCategoriesCreateComponent } from "./symptom-categories-create.component";

describe('SymptomCategoriesCreateComponent', () => {
  let component: SymptomCategoriesCreateComponent;
  let fixture: ComponentFixture<SymptomCategoriesCreateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SymptomCategoriesCreateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SymptomCategoriesCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
