import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PerformanceLevelsComponent } from './performance-levels.component';

describe('PerformanceLevelsComponent', () => {
  let component: PerformanceLevelsComponent;
  let fixture: ComponentFixture<PerformanceLevelsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PerformanceLevelsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PerformanceLevelsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
