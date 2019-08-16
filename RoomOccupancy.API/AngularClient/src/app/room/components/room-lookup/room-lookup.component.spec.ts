import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RoomLookupComponent } from './room-lookup.component';

describe('RoomLookupComponent', () => {
  let component: RoomLookupComponent;
  let fixture: ComponentFixture<RoomLookupComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RoomLookupComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RoomLookupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
