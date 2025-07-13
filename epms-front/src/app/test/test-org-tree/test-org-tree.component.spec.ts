import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TestOrgTreeComponent } from './test-org-tree.component';

describe('TestOrgTreeComponent', () => {
  let component: TestOrgTreeComponent;
  let fixture: ComponentFixture<TestOrgTreeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TestOrgTreeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TestOrgTreeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
