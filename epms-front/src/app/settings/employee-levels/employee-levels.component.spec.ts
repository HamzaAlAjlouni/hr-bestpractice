import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeeLevelsComponent } from './employee-levels.component';

describe('EmployeeLevelsComponent', () => {
  let component: EmployeeLevelsComponent;
  let fixture: ComponentFixture<EmployeeLevelsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EmployeeLevelsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EmployeeLevelsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
