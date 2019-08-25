

export class ScheduleLookupModel {
  Id: number;
  start: string;
  end: Date;
  subject: Date;
  reservationDays: number[];
  isCyclical: boolean;
  cancelationDateTime: Date;
  roomId: number;
  roomName: string;
}

export class ScheduleViewModel {
  public resevations: ScheduleLookupModel[];
}
