import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { addcorporateclientComponent } from './addcorporateclient.component';

describe('addcorporateclientComponent', () => {
  let component: addcorporateclientComponent;
  let fixture: ComponentFixture<addcorporateclientComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [addcorporateclientComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(addcorporateclientComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
