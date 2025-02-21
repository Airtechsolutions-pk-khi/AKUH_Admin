import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AppsettingComponent } from '././appsettings.component';

describe('AppsettingsComponent', () => {
  let component: AppsettingComponent;
  let fixture: ComponentFixture<AppsettingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [AppsettingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AppsettingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
