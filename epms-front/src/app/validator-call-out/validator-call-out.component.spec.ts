import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ValidatorCallOutComponent } from './validator-call-out.component';

describe('ValidatorCallOutComponent', () => {
  let component: ValidatorCallOutComponent;
  let fixture: ComponentFixture<ValidatorCallOutComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ValidatorCallOutComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ValidatorCallOutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
