import { RoomDetailModel } from './../../../models/campus/room-detail-model';
import { RoomsService } from './../../../services/rooms.service';
import { Component, OnInit, Input } from '@angular/core';
import { Equipment } from 'src/app/models/equipment.model';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-room-details',
  templateUrl: './room-details.component.html',
  styleUrls: ['./room-details.component.scss']
})
export class RoomDetailsComponent implements OnInit {

  constructor(private roomsService: RoomsService) {

  }
  public dataSource: RoomDetailModel;
  @Input()
  public set room(v: RoomDetailModel) {
    this.dataSource = v;
  }
  ngOnInit() {

  }

}
