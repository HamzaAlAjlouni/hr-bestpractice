import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ActionPlansAssessmentComponent } from './action-plans-assessment.component';

describe('ActionPlansAssessmentComponent', () => {
  let component: ActionPlansAssessmentComponent;
  let fixture: ComponentFixture<ActionPlansAssessmentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ActionPlansAssessmentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ActionPlansAssessmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
