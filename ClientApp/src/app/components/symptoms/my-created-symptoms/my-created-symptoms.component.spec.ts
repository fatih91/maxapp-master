import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MyCreatedSymptomsComponent } from './my-created-symptoms.component';

describe('MyCreatedSymptomsComponent', () => {
  let component: MyCreatedSymptomsComponent;
  let fixture: ComponentFixture<MyCreatedSymptomsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MyCreatedSymptomsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MyCreatedSymptomsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
