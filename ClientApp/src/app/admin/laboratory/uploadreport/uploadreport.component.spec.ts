import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UploadreportComponent } from './uploadreport.component';

describe('UploadreportComponent', () => {
  let component: UploadreportComponent;
  let fixture: ComponentFixture<UploadreportComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UploadreportComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UploadreportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
