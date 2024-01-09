import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LaboratoryinquiryComponent } from './laboratoryinquiry.component';

describe('LaboratoryinquiryComponent', () => {
  let component: LaboratoryinquiryComponent;
  let fixture: ComponentFixture<LaboratoryinquiryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LaboratoryinquiryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LaboratoryinquiryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
