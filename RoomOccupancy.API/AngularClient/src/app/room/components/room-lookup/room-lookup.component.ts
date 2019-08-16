import { IRoomLookupModel } from './../../../models/campus/room-lookup.model';
import { RoomsService } from './../../../services/rooms.service';
import { Component, OnInit } from '@angular/core';
import {switchMap} from 'rxjs/operators'; 
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
@Component({
  selector: 'app-room-lookup',
  templateUrl: './room-lookup.component.html',
  styleUrls: ['./room-lookup.component.scss']
})
export class RoomLookupComponent implements OnInit {

  private rooms: IRoomLookupModel[]; 
  private buildingNo: number; 

  constructor(
    private readonly roomsService: RoomsService,
    private readonly route : ActivatedRoute
  ) { }


  /**
   * Download the list of rooms for building with number taken from the id route parameter.
   */
  public GetBuildingRooms() {
     this.route.paramMap.pipe(
      switchMap(params => {
        // (+) converts string into number
        this.buildingNo = + params.get('id');
        return this.roomsService.getBuildingRooms(this.buildingNo); 
      })
    ).subscribe(x => {
      this.rooms = x.rooms;
       console.log(`GetBuildingRooms call with ${this.buildingNo} parameter returned ${this.rooms.length} elements.`);
    });
  }

  ngOnInit() {
    this.GetBuildingRooms();
  }

}
