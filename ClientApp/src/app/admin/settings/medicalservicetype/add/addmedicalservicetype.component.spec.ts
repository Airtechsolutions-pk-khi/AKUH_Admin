import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { AddMedicalServicetypeComponent } from './addmedicalservicetype.component';



describe('AddMedicalServicetypeComponent', () => {
  let component: AddMedicalServicetypeComponent;
  let fixture: ComponentFixture<AddMedicalServicetypeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [AddMedicalServicetypeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddMedicalServicetypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
