import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SymptomFilterListComponent } from './symptom-filter-list.component';

describe('SymptomFilterListComponent', () => {
  let component: SymptomFilterListComponent;
  let fixture: ComponentFixture<SymptomFilterListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SymptomFilterListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SymptomFilterListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
