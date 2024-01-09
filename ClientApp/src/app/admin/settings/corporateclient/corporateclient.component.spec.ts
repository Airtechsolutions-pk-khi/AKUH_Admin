import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CorporateClientComponent } from './corporateclient.component';

describe('CorporateClientComponent', () => {
  let component: CorporateClientComponent;
  let fixture: ComponentFixture<CorporateClientComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [CorporateClientComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CorporateClientComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
