import { async, ComponentFixture, TestBed } from "@angular/core/testing";

import { SymptomCategoriesViewComponent } from './symptom-categories-view.component';

describe('SymptomCategoriesViewComponent', () => {
  let component: SymptomCategoriesViewComponent;
  let fixture: ComponentFixture<SymptomCategoriesViewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SymptomCategoriesViewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SymptomCategoriesViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
