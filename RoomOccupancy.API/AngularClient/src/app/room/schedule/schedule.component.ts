import { ScheduleLookupModel } from './../../models/schedule/schedule-lookup';
import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { MatTableDataSource, MatSort, MatPaginator } from '@angular/material';
import { RoomsService } from '../../services/rooms.service';
import { ToastrService } from 'ngx-toastr';
import { filter } from 'minimatch';

@Component({
  selector: 'app-schedule',
  templateUrl: './schedule.component.html',
  styleUrls: ['./schedule.component.scss']
})
export class ScheduleComponent implements OnInit {

  private _roomId: number;

  private _schedule: ScheduleLookupModel[];

  public dataSource: ScheduleLookupModel[] = [];
  
  constructor(
    private readonly roomsService: RoomsService,
    private toastr: ToastrService) {
     
  }
  public get roomId(): number {
    return this._roomId;
  }
  @Input()
  public set roomId(v: number) {
    if(v == this._roomId)
      return;

    this._roomId = v;
    this.subscribeSchedule();
  }

  private subscribeSchedule(){
     this.roomsService.getSchedule(this.roomId)
      .subscribe(x => {
        this.toastr.info(JSON.stringify(x));
      });
  }


  public applyFilter(filterValue: string) {
    this.dataSource = this._schedule
        .filter(x => x.start.indexOf(filterValue) > 0);
  }
  ngOnInit() {
    
  }

}
