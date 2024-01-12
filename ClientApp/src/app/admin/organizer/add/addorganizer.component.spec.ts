import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddOrganizerComponent } from '././addorganizer.component';

describe('AddOrganizerComponent', () => {
  let component: AddOrganizerComponent;
  let fixture: ComponentFixture<AddOrganizerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [AddOrganizerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddOrganizerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
