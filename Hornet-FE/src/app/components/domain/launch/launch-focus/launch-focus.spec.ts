import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LaunchFocus } from './launch-focus';

describe('LaunchFocus', () => {
  let component: LaunchFocus;
  let fixture: ComponentFixture<LaunchFocus>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LaunchFocus],
    }).compileComponents();

    fixture = TestBed.createComponent(LaunchFocus);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
