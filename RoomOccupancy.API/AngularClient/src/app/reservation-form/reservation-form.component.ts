import { ReservationSearchParams } from './../models/reservation-search-params.model';
import { Component, OnInit } from '@angular/core';
@Component({
  selector: 'app-reservation-form',
  templateUrl: './reservation-form.component.html',
  styleUrls: ['./reservation-form.component.scss']
})
export class ReservationFormComponent implements OnInit {
  public args : ReservationSearchParams;
  constructor() { }

  ngOnInit() {
  }

}