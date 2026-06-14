import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ById } from './by-id';

describe('ById', () => {
  let component: ById;
  let fixture: ComponentFixture<ById>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ById],
    }).compileComponents();

    fixture = TestBed.createComponent(ById);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
