import { IRoomLookupModel } from './../../models/campus/room-lookup.model';
import { Component, OnInit, OnChanges } from '@angular/core';

@Component({
  selector: 'app-find-room',
  templateUrl: './find-room.component.html',
  styleUrls: ['./find-room.component.scss']
})
export class FindRoomComponent implements OnInit {

  protected _rooms: IRoomLookupModel[] = [];
  public set rooms(v:IRoomLookupModel[]){
    console.log(v);
    this._rooms = v;
  }
  public get rooms(){
    return this._rooms;
  }
  constructor() { }
  public onRoomsEmitted(e){
    this.rooms = e;
  }
  ngOnInit() {
  }

}
