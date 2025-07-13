import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SkillstypesComponent } from './skillstypes.component';

describe('SkillstypesComponent', () => {
  let component: SkillstypesComponent;
  let fixture: ComponentFixture<SkillstypesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SkillstypesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SkillstypesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
