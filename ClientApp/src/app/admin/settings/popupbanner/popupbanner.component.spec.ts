import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { PopupBannerComponent } from './popupbanner.component';




describe('PopupBannerComponent', () => {
  let component: PopupBannerComponent;
  let fixture: ComponentFixture<PopupBannerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [PopupBannerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PopupBannerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
