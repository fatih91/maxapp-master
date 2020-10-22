import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DiagnoseCreateComponent } from './diagnose-create.component';

describe('DiagnoseCreateComponent', () => {
  let component: DiagnoseCreateComponent;
  let fixture: ComponentFixture<DiagnoseCreateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DiagnoseCreateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DiagnoseCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
