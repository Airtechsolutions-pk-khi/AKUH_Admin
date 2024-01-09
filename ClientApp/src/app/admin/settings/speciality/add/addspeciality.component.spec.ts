import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddSpecialityComponent } from './addspeciality.component';

describe('AddSpecialityComponent', () => {
  let component: AddSpecialityComponent;
  let fixture: ComponentFixture<AddSpecialityComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [AddSpecialityComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddSpecialityComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
