import { ScheduleLookupModel, ScheduleViewModel } from './../../models/schedule/schedule-lookup';
import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { RoomsService } from '../../services/rooms.service';
import { ToastrService } from 'ngx-toastr';

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
    private toastr: ToastrService) {
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
    this.applyFilter();
  }

  public applyFilter() {
    if(this._schedule.reservations.length < 1)
    {
      this.toastr.info("Brak dostępnych rezerwacji.")
    }


    this.dataSource = this._schedule.reservations;
  }
  public onShowAllClick(event) {
    console.log(this.showAll);
  }
  public setRowIndex(i: number) {
    this.rowIndex = i;
  }
  ngOnInit() {
  }

}
