import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DiagnosticCategoriesComponent } from './diagnosticcategories.component';

describe('UploadreportComponent', () => {
  let component: DiagnosticCategoriesComponent;
  let fixture: ComponentFixture<DiagnosticCategoriesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [DiagnosticCategoriesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DiagnosticCategoriesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
