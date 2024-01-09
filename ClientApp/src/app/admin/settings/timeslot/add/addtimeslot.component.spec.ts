import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddTimeSlotComponent } from './addtimeslot.component';

describe('AddTimeSlotComponent', () => {
  let component: AddTimeSlotComponent;
  let fixture: ComponentFixture<AddTimeSlotComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [AddTimeSlotComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddTimeSlotComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
