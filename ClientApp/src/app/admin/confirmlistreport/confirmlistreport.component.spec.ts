import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ConfirmlistreportComponent } from './confirmlistreport.component';



describe('ConfirmlistreportComponent', () => {
  let component: ConfirmlistreportComponent;
  let fixture: ComponentFixture<ConfirmlistreportComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ConfirmlistreportComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ConfirmlistreportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
