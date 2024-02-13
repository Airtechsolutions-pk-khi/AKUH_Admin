import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { OrganisingCommitteeComponent } from './organisingcommittee.component';



describe('SpeakerComponent', () => {
  let component: OrganisingCommitteeComponent;
  let fixture: ComponentFixture<OrganisingCommitteeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [OrganisingCommitteeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OrganisingCommitteeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
