

export class ScheduleLookupModel{
    Id:number;
    start:string;
    end:string;
    subject:string;
    reservationDays: number[];
    isCyclical: boolean;
    cancelationDateTime: string;
    roomId:number;
    roomName: string;
}

export class ScheduleViewModel{
  public resevations: ScheduleLookupModel[];
}