import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NursingAppointmentComponent } from './nursingappointment.component';

describe('NursingAppointmentComponent', () => {
  let component: NursingAppointmentComponent;
  let fixture: ComponentFixture<NursingAppointmentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NursingAppointmentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NursingAppointmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
