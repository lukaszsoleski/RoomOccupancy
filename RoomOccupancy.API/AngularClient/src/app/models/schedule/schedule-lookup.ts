

export class ScheduleLookupModel {
  Id: number;
  start: Date;
  end: Date;
  subject: string;
  reservationDays: number[];
  isCyclical: boolean;
  roomId: number;
  roomName: string;
  awaitsAcceptance: boolean;
  userName: string;
}

export class ScheduleViewModel {
  public reservations: ScheduleLookupModel[];
}
