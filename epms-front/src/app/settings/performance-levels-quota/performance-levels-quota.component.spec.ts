import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PerformanceLevelsQuotaComponent } from './performance-levels-quota.component';

describe('PerformanceLevelsQuotaComponent', () => {
  let component: PerformanceLevelsQuotaComponent;
  let fixture: ComponentFixture<PerformanceLevelsQuotaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PerformanceLevelsQuotaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PerformanceLevelsQuotaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
