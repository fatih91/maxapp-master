import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SymptomTableComponent } from './symptom-table.component';

describe('SymptomTableComponent', () => {
  let component: SymptomTableComponent;
  let fixture: ComponentFixture<SymptomTableComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SymptomTableComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SymptomTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
