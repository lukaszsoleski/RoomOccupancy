
export interface IRoomLookupModel {
  id: number;
  label: string;
  actualUse: string;
  facultyLookup: string;
  seats: number;
}
export interface IRoomListViewModel{
  rooms: IRoomLookupModel[];
}
export class RoomListViewModel implements IRoomListViewModel {
 public rooms: IRoomLookupModel[];

 /**
  *
  */
 constructor() {
  this.rooms = [];
 }
}
