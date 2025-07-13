import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TrafficlightsetupComponent } from './trafficlightsetup.component';

describe('TrafficlightsetupComponent', () => {
  let component: TrafficlightsetupComponent;
  let fixture: ComponentFixture<TrafficlightsetupComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TrafficlightsetupComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TrafficlightsetupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
