import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { StrategyComparisonComponent } from './strategy-comparison.component';

describe('ProjectsAssessmentComponent', () => {
  let component: StrategyComparisonComponent;
  let fixture: ComponentFixture<StrategyComparisonComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StrategyComparisonComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StrategyComparisonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
