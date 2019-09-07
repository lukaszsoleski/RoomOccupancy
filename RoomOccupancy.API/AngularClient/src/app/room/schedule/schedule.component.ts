import { ScheduleLookupModel, ScheduleViewModel } from './../../models/schedule/schedule-lookup';
import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { RoomsService } from '../../services/rooms.service';
import { ToastrService } from 'ngx-toastr';
import * as moment from 'moment';
import { DatePipe } from '@angular/common';
import { Reservation } from 'src/app/models/schedule/reservation';
import { NgxSpinnerService } from 'ngx-spinner';
@Component({
  selector: 'app-schedule',
  templateUrl: './schedule.component.html',
  styleUrls: ['./schedule.component.scss']
})
export class ScheduleComponent implements OnInit {

  // tslint:disable-next-line:variable-name
  private _roomId: number;

  // tslint:disable-next-line:variable-name
  private _schedule: ScheduleViewModel;
  protected dataSource: ScheduleLookupModel[] = [];
  protected dateFilter: Date;
  protected isDateEmpty: boolean;
  protected rowIndex: number;
  protected showAll = false;
  public isCollapsed = true;
  constructor(
    private readonly roomsService: RoomsService,
    private toastr: ToastrService,
    private datepipe: DatePipe,
    private spinner: NgxSpinnerService) {
    this.dateFilter = new Date();
  }
  public get roomId(): number {
    return this._roomId;
  }
  @Input()
  public set roomId(v: number) {
    if (v === this._roomId) {
      return;
    }
    this._roomId = v;
    this.getSchedule();
  }

  private getSchedule() {
    this.spinner.show();
    this.roomsService.getSchedule(this.roomId)
      .subscribe(x => {
        this._schedule = x;
        this.applyFilter();
        this.spinner.hide();
      }, () => this.spinner.hide());
  }
  public dateFilterChanged(event) {
    if (this.dateFilter == null) {
      this.showAll = true;
    }
    this.applyFilter();
  }

  public applyFilter() {
    if (this._schedule.reservations.length < 1) {
      this.toastr.info('Brak dostępnych rezerwacji.');
      return;
    }
    if (this.showAll === true) {
      this.dataSource = this._schedule.reservations;
      this.toastr.info('Wszystkie aktywne rezerwacje.');
      return;
    } else {
      this.dataSource = this.getFilteredReservations(this.dateFilter);
    }
  }
  private getFilteredReservations(selectedDate: Date): ScheduleLookupModel[] {
    // tslint:disable-next-line:prefer-const
    let reservations = [];
    for (const reservation of this._schedule.reservations) {
      // the selected date is within the scope of the booking period and
      // and the day of the week matches
      if (moment(selectedDate).isBetween(reservation.start, reservation.end, 'day', '[]')) {
        if (reservation.isCyclical
          && (reservation.reservationDays.indexOf(moment(selectedDate).weekday()) >= 0)) {
          // tslint:disable-next-line:no-debugger
          // debugger;
          reservations.push(reservation);
        } else if (!reservation.isCyclical) {
          if (moment(reservation.start).isSame(moment(selectedDate), 'day')) {
            // tslint:disable-next-line:no-debugger
            // debugger;
            reservations.push(reservation);
          }
        }
      }
    }
    if (reservations.length < 1) {
      this.toastr.info(`Obecnie nie ma rezerwacji w terminie ${this.datepipe.transform(selectedDate, 'dd/MM/yyyy')}`);
    }
    return reservations;
  }

  public postReservation(e: Reservation) {
    let reservation = e;
    reservation.roomId = this.roomId;
    this.roomsService.postReservation(reservation).subscribe(x => {
      this.toastr.success('Dodano rezerwacje!');
      this.getSchedule();
      this.isCollapsed = true;
    });
  }

  public onShowAllClick(event) {
    this.applyFilter();
  }
  public setRowIndex(i: number) {
    this.rowIndex = i;
  }
  ngOnInit() {
  }

}
