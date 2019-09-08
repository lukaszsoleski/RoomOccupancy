

export class ScheduleLookupModel {
  id: number;
  start: Date;
  end: Date;
  subject: string;
  reservationDays: number[];
  isCyclical: boolean;
  isCancelled: boolean;
  roomId: number;
  roomName: string;
  awaitsAcceptance: boolean;
  userName: string;
  appUserId: string;
}

export class ScheduleViewModel {
  public currentUserId: string;
  public reservations: ScheduleLookupModel[];
}
