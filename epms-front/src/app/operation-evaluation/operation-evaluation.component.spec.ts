import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OperationEvaluationComponent } from './operation-evaluation.component';

describe('OperationEvaluationComponent', () => {
  let component: OperationEvaluationComponent;
  let fixture: ComponentFixture<OperationEvaluationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OperationEvaluationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OperationEvaluationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
