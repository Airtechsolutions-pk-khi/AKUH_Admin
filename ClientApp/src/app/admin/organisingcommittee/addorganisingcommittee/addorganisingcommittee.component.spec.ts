import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddOrganisingCommitteeComponent } from './addorganisingcommittee.component';

describe('AddSpeakerComponent', () => {
  let component: AddOrganisingCommitteeComponent;
  let fixture: ComponentFixture<AddOrganisingCommitteeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [AddOrganisingCommitteeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddOrganisingCommitteeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
