import { ScheduleLookupModel, ScheduleViewModel } from './../../models/schedule/schedule-lookup';
import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { RoomsService } from '../../services/rooms.service';
import { ToastrService } from 'ngx-toastr';
import * as moment from 'moment';
import { DatePipe } from "@angular/common";
@Component({
  selector: 'app-schedule',
  templateUrl: './schedule.component.html',
  styleUrls: ['./schedule.component.scss']
})
export class ScheduleComponent implements OnInit {

  private _roomId: number;

  private _schedule: ScheduleViewModel;

  protected dataSource: ScheduleLookupModel[] = [];
  protected dateFilter: Date;
  protected isDateEmpty: boolean;
  protected rowIndex: number;
  protected showAll = false;
  constructor(
    private readonly roomsService: RoomsService,
    private toastr: ToastrService,
    private datepipe : DatePipe) {
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
    this.roomsService.getSchedule(this.roomId)
      .subscribe(x => {
        this._schedule = x;
        this.applyFilter();
      });
  }
  public dateFilterChanged(event) {
    if (this.dateFilter == null) {
      this.showAll = true;
    }
    this.applyFilter();
  }

  public applyFilter(){
    if (this._schedule.reservations.length < 1) {
      this.toastr.info("Brak dostępnych rezerwacji.")
      return;
    }
    if (this.showAll === true) {
      this.dataSource = this._schedule.reservations;
      this.toastr.info("Wszystkie aktywne rezerwacje.");
      return;
    }
    else {
     this.dataSource = this.getFilteredReservations(this.dateFilter);
    }
  }
  private getFilteredReservations(selectedDate : Date) : ScheduleLookupModel[]{
    
    let reservations = [];
    for (const reservation of this._schedule.reservations) {
      // the selected date is within the scope of the booking period and 
      // and the day of the week matches
      if (reservation.isCyclical
         && moment(selectedDate).isBetween(reservation.start,reservation.cancelationDateTime,'day','[]') 
          &&  (reservation.reservationDays.indexOf(moment(selectedDate).weekday()) >= 0)){ 
            reservations.push(reservation);
          }
      else{
        if(moment(reservation.start).isSame(moment(selectedDate),'day')){
          reservations.push(reservation);
        }
      }
    }
    if(reservations.length < 1)
      {
        this.toastr.info(`Obecnie nie ma rezerwacji w terminie ${this.datepipe.transform(selectedDate, 'dd/MM/yyyy')}`);
      }
    return reservations;
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
