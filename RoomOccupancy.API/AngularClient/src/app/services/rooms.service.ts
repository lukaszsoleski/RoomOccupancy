import { Observable } from 'rxjs';
import { RoomListViewModel } from './../models/campus/room-lookup.model';
import { HttpClientUtilsService } from './../common/http-client-utils.service';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class RoomsService {
  
  private getAllRoute = 'rooms/building';

  constructor(private httpService: HttpClientUtilsService) { }

  public getRooms(buildingNo?: number): Observable<RoomListViewModel> {
    let route = this.getAllRoute;
    if(buildingNo != null || buildingNo != undefined){
      route += `?number=${buildingNo}`;
    }
    return this.httpService.get<RoomListViewModel>(route);
  }
  
}
