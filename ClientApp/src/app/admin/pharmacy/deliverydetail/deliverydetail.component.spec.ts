import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DeliverydetailComponent } from './deliverydetail.component';

describe('DeliverydetailComponent', () => {
  let component: DeliverydetailComponent;
  let fixture: ComponentFixture<DeliverydetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DeliverydetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DeliverydetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
