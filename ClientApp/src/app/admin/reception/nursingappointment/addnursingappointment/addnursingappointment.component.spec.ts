import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddnursingappointmentComponent } from './addnursingappointment.component';

describe('AddnursingappointmentComponent', () => {
  let component: AddnursingappointmentComponent;
  let fixture: ComponentFixture<AddnursingappointmentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddnursingappointmentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddnursingappointmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
