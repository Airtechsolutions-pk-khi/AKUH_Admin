import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddpromotionComponent } from './addpromotion.component';

describe('AddpromotionComponent', () => {
  let component: AddpromotionComponent;
  let fixture: ComponentFixture<AddpromotionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [AddpromotionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddpromotionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
