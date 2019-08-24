import { ScheduleLookupModel } from './../../models/schedule/schedule-lookup';
import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { MatTableDataSource, MatSort, MatPaginator } from '@angular/material';
import { RoomsService } from '../../services/rooms.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-schedule',
  templateUrl: './schedule.component.html',
  styleUrls: ['./schedule.component.scss']
})
export class ScheduleComponent implements OnInit {

  private _roomId: number;

  @ViewChild(MatSort, { static: true }) sort: MatSort;
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;

  public dataSource: MatTableDataSource<ScheduleLookupModel>;
  public displayedColumns: string[] = ['label', 'actualUse', 'seats', 'facultyLookup'];

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
        this.dataSource.data = x.resevations;
        this.toastr.info(JSON.stringify(x));
      });
  }


  public applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }
  ngOnInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
    
  }

}
