import { ScheduleLookupModel } from './../../models/schedule/schedule-lookup';
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

  private _schedule: ScheduleLookupModel[];

  public dataSource: ScheduleLookupModel[] = [];
  public dateFilter = new Date();
  public rowIndex = 0;
  constructor(
    private readonly roomsService: RoomsService,
    private toastr: ToastrService) {

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
    this.subscribeSchedule();
  }

  private subscribeSchedule() {
    this.roomsService.getSchedule(this.roomId)
      .subscribe(x => {
        this.toastr.info(JSON.stringify(x));
      });
  }
  public dateFilterChanged($event) {
    this.toastr.info($event.value);
    this.toastr.info(this.dateFilter.toDateString());
  }

  public applyFilter(filterValue: string) {
    this.dataSource = this._schedule
      .filter(x => x.start.indexOf(filterValue) > 0);
  }
  public setRowIndex(i: number) {
    this.rowIndex = i;
  }
  ngOnInit() {
    this.dataSource.push(new ScheduleLookupModel());
    this.dataSource.push(new ScheduleLookupModel());
    this.dataSource.push(new ScheduleLookupModel());
    this.dataSource.push(new ScheduleLookupModel());
  }

}
