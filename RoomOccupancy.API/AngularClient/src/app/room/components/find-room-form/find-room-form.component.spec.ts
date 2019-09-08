import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FindRoomFormComponent } from './find-room-form.component';

describe('FindRoomFormComponent', () => {
  let component: FindRoomFormComponent;
  let fixture: ComponentFixture<FindRoomFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FindRoomFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FindRoomFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
