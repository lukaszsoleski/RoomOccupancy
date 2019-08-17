import { Component, OnInit, ViewChild, AfterViewInit, AfterContentInit } from '@angular/core';
import { ReservationSearchParams } from '../models/reservation-search-params.model';
import { RoomLookupComponent } from '../room/components/room-lookup/room-lookup.component';
import { ActivatedRoute } from '@angular/router';
import { switchMap } from 'rxjs/operators';
import { Observable } from 'rxjs';
@Component({
  selector: 'app-building',
  templateUrl: './building.component.html',
  styleUrls: ['./building.component.scss']
})
export class BuildingComponent implements OnInit {

  public selectedBuilding: string;
  public reservationSearchParams: ReservationSearchParams;
  
  constructor( private readonly route : ActivatedRoute) { }

  ngOnInit() {
   this.route.paramMap.subscribe(params => this.selectedBuilding = params.get('id'));
  }
}
