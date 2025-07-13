import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CalculatationSetupComponent } from './calculatation-setup.component';

describe('CalculatationSetupComponent', () => {
  let component: CalculatationSetupComponent;
  let fixture: ComponentFixture<CalculatationSetupComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CalculatationSetupComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CalculatationSetupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
