import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddEventCategoryComponent } from './addeventcategory.component';

describe('AddEventCategoryComponent', () => {
  let component: AddEventCategoryComponent;
  let fixture: ComponentFixture<AddEventCategoryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [AddEventCategoryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddEventCategoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
