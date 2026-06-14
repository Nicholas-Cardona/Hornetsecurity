import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LaunchGrid } from './launch-grid';

describe('LaunchGrid', () => {
  let component: LaunchGrid;
  let fixture: ComponentFixture<LaunchGrid>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LaunchGrid],
    }).compileComponents();

    fixture = TestBed.createComponent(LaunchGrid);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
