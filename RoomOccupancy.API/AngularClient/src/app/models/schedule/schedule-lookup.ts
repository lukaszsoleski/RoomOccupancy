

export class ScheduleLookupModel {
  Id: number;
  start: string;
  end: Date;
  subject: Date;
  reservationDays: number[];
  isCyclical: boolean;
  roomId: number;
  roomName: string;
  createdBy: string;
}

export class ScheduleViewModel {
  public reservations: ScheduleLookupModel[];
}
