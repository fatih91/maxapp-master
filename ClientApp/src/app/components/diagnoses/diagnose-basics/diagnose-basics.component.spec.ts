import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DiagnoseBasicsComponent } from './diagnose-basics.component';

describe('DiagnoseBasicsComponent', () => {
  let component: DiagnoseBasicsComponent;
  let fixture: ComponentFixture<DiagnoseBasicsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DiagnoseBasicsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DiagnoseBasicsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
