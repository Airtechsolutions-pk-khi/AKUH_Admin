import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { AddPopupBannerComponent } from './addpopupbanner.component';




describe('AddPopupBannerComponent', () => {
  let component: AddPopupBannerComponent;
  let fixture: ComponentFixture<AddPopupBannerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [AddPopupBannerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddPopupBannerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
