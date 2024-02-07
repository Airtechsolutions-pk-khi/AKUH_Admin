import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { EventattendeedetailsComponent } from './eventattendeedetails.component';

 

describe('OrderdetailsComponent', () => {
  let component: EventattendeedetailsComponent;
  let fixture: ComponentFixture<EventattendeedetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [EventattendeedetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EventattendeedetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
