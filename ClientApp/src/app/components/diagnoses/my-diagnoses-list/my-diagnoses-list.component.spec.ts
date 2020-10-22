import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MyDiagnosesListComponent } from './my-diagnoses-list.component';

describe('MyDiagnosesListComponent', () => {
  let component: MyDiagnosesListComponent;
  let fixture: ComponentFixture<MyDiagnosesListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MyDiagnosesListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MyDiagnosesListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
