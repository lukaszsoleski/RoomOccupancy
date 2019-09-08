import { ToastrService } from 'ngx-toastr';
import { IRoomLookupModel } from './../models/campus/room-lookup.model';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { switchMap } from 'rxjs/operators';
import { RoomsService } from '../services/rooms.service';
import { RoomDetailModel } from '../models/campus/room-detail-model';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-room',
  templateUrl: './room.component.html',
  styleUrls: ['./room.component.scss']
})
export class RoomComponent implements OnInit {

  protected room: RoomDetailModel;
  protected routeId: number;
  constructor(
    private readonly route: ActivatedRoute,
    private readonly roomsService: RoomsService,
    private toastr: ToastrService

  ) { this.room = new RoomDetailModel(); }

  getRoom() {
    this.route.paramMap.pipe(
      switchMap(params => {
        // (+) converts string into number
        const id = +params.get('id');
        this.routeId = id;
        return this.roomsService.getRoom(id);
      })
    ).subscribe(x => {

      if (!x) {
        this.toastr.info(`Brak informacji o pokoju z id ${this.routeId} 🤯`);
      }
      this.room = x;
    });
  }

  ngOnInit() {
    this.getRoom();
  }

}
