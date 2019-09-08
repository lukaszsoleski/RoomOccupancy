import { Equipment } from 'src/app/models/equipment.model';
import { map, tap } from 'rxjs/operators';
import { Reservation } from './../models/schedule/reservation';
import { RoomDetailModel } from './../models/campus/room-detail-model';
import { Observable } from 'rxjs';
import { RoomListViewModel } from './../models/campus/room-lookup.model';
import { HttpClientUtilsService } from './../common/http-client-utils.service';
import { Injectable } from '@angular/core';
import { ScheduleViewModel } from '../models/schedule/schedule-lookup';
import { Faculty } from '../models/faculty.model';

@Injectable({
  providedIn: 'root'
})
export class RoomsService {

  private roomsRoute = 'room/building';

  constructor(private httpService: HttpClientUtilsService) { }

  public getRooms(buildingNo?: number): Observable<RoomListViewModel> {
    let route = this.roomsRoute;
    if(buildingNo != null || buildingNo != undefined){
      route += `?number=${buildingNo}`;
    }
    return this.httpService.get<RoomListViewModel>(route);
  }
  public getSchedule(roomNo: number): Observable<ScheduleViewModel>{
    return this.httpService
      .get<ScheduleViewModel>(`schedule/room/${roomNo}`).pipe(
        tap(x => console.log(x))
        );
  }
  public getEquipment(roomId: number): Observable<Equipment[]> {
    return this.httpService.get(`room/${roomId}/equipment`);
  }
  public getRoom(id: number): Observable<RoomDetailModel> {
    return this.httpService.get<RoomDetailModel>(`room/${id}`);
  }
  public postReservation(reservation: Reservation): Observable<unknown> {
    return this.httpService.post('reservation/', reservation);
  }
  public getFaculties(): Observable<Faculty[]>{
    return this.httpService.get('faculty');
  }
  public cancelReservation(id: number) {
    return this.httpService.delete(`reservation/${id}`);
  }

}
