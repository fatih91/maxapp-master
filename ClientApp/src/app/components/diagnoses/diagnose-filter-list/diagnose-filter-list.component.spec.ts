import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DiagnoseFilterListComponent } from './diagnose-filter-list.component';

describe('DiagnoseFilterListComponent', () => {
  let component: DiagnoseFilterListComponent;
  let fixture: ComponentFixture<DiagnoseFilterListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DiagnoseFilterListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DiagnoseFilterListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
