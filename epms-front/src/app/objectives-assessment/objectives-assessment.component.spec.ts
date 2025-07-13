import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ObjectivesAssessmentComponent } from './objectives-assessment.component';

describe('ObjectivesAssessmentComponent', () => {
  let component: ObjectivesAssessmentComponent;
  let fixture: ComponentFixture<ObjectivesAssessmentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ObjectivesAssessmentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ObjectivesAssessmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
