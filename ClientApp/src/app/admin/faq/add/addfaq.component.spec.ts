import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddFaqComponent } from './addfaq.component';

describe('AddFaqComponent', () => {
  let component: AddFaqComponent;
  let fixture: ComponentFixture<AddFaqComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [AddFaqComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddFaqComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
