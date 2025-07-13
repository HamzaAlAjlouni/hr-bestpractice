import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ObjectiveKPIAssessmentComponent } from './objective-kpi-assessment.component';

describe('ObjectiveKPIAssessmentComponent', () => {
  let component: ObjectiveKPIAssessmentComponent;
  let fixture: ComponentFixture<ObjectiveKPIAssessmentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ObjectiveKPIAssessmentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ObjectiveKPIAssessmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
