import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { MedicalServicetypeComponent } from './medicalservicetype.component';



describe('MedicalServicetypeComponent', () => {
  let component: MedicalServicetypeComponent;
  let fixture: ComponentFixture<MedicalServicetypeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [MedicalServicetypeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MedicalServicetypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
