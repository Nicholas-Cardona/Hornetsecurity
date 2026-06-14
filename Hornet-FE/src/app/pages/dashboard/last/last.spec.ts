import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Last } from './last';

describe('Last', () => {
  let component: Last;
  let fixture: ComponentFixture<Last>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Last],
    }).compileComponents();

    fixture = TestBed.createComponent(Last);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
