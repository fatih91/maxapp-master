import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SymptomViewComponent } from './symptom-view.component';

describe('SymptomViewComponent', () => {
  let component: SymptomViewComponent;
  let fixture: ComponentFixture<SymptomViewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SymptomViewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SymptomViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
