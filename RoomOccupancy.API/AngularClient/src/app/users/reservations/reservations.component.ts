import { NgxSpinnerService } from 'ngx-spinner';
import { ScheduleLookupModel } from './../../models/schedule/schedule-lookup';
import { Component, OnInit, Input } from '@angular/core';
import { RoomsService } from 'src/app/services/rooms.service';
import { UsersService } from 'src/app/services/users.service';
import * as moment from 'moment';
@Component({
  selector: 'app-reservations',
  templateUrl: './reservations.component.html',
  styleUrls: ['./reservations.component.scss']
})
export class ReservationsComponent implements OnInit {

  protected displayedColumns: string[] = ['position', 'time', 'subject', 'roomName', 'status'];
  protected dataSource: ScheduleLookupModel[] = [];

  constructor(private userService: UsersService, private spinner: NgxSpinnerService) { }

  protected getUserReservations() {
    this.spinner.show();
    this.userService.getReservations().subscribe(x => {
      console.log(x);
      this.dataSource = x;
      this.spinner.hide();
    }, () => this.spinner.hide());
  }
  protected getEndTime(r: ScheduleLookupModel): string {
    // should use timespan
    if (moment(r.start).isDST() && moment(r.end).isDST() === false) {
      return moment(r.end).add(1, 'hour').format('HH:mm');
    }

    return moment(r.end).format('HH:mm');
  }
  protected getReservationStatus(r: ScheduleLookupModel): string{
    if( r.isCancelled) { return 'Anulowane'; }

    return r.awaitsAcceptance ? 'W akceptacji' : 'Zatwierdzone';
  }
  ngOnInit() {
    this.getUserReservations();
  }
}
