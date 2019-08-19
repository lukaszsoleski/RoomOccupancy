import { Observable } from 'rxjs';
import { RoomListViewModel } from './../models/campus/room-lookup.model';
import { HttpClientUtilsService } from './../common/http-client-utils.service';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class RoomsService {

  constructor(private httpService: HttpClientUtilsService) { }

  /**
   * getRoomsInBuilding
   */
  public getBuildingRooms(buildingNo: number): Observable<RoomListViewModel> {
    return this.httpService.get<RoomListViewModel>(`rooms/frombuilding?buildingno=${buildingNo}`);
  }
  public getRooms(): Observable<RoomListViewModel> {
    return this.httpService.get<RoomListViewModel>('rooms/frombuilding');
  }
}
