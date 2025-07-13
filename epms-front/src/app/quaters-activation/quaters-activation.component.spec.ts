import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { QuatersActivationComponent } from './quaters-activation.component';

describe('QuatersActivationComponent', () => {
  let component: QuatersActivationComponent;
  let fixture: ComponentFixture<QuatersActivationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ QuatersActivationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(QuatersActivationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
