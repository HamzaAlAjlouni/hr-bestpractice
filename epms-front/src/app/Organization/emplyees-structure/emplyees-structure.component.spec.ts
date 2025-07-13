import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EmplyeesStructureComponent } from './emplyees-structure.component';

describe('EmplyeesStructureComponent', () => {
  let component: EmplyeesStructureComponent;
  let fixture: ComponentFixture<EmplyeesStructureComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EmplyeesStructureComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EmplyeesStructureComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
