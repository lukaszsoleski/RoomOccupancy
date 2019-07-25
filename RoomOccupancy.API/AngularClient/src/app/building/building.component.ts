import { Component, OnInit } from '@angular/core';
import { ReservationSearchParams } from '../models/reservation-search-params.model';
@Component({
  selector: 'app-building',
  templateUrl: './building.component.html',
  styleUrls: ['./building.component.scss']
})
export class BuildingComponent implements OnInit {

  public reservationSearchParams: ReservationSearchParams;
  constructor() { }

  ngOnInit() {
  }
}
