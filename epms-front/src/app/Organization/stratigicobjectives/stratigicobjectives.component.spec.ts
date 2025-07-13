import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { StratigicObjectivesComponent } from './stratigicobjectives.component';

describe('StratigicObjectivesComponent', () => {
  let component: StratigicObjectivesComponent;
  let fixture: ComponentFixture<StratigicObjectivesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StratigicObjectivesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StratigicObjectivesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
