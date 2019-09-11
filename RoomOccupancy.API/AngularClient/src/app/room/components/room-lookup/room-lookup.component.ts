import { IRoomLookupModel, RoomListViewModel } from './../../../models/campus/room-lookup.model';
import { RoomsService } from './../../../services/rooms.service';
import { Component, OnInit, ViewChild, Output, Input, OnChanges, SimpleChange } from '@angular/core';
import { switchMap } from 'rxjs/operators';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { MatTableDataSource, MatSort, MatPaginator } from '@angular/material';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import {SimpleChanges} from '@angular/core';
@Component({
  selector: 'app-room-lookup',
  templateUrl: './room-lookup.component.html',
  styleUrls: ['./room-lookup.component.scss']
})
export class RoomLookupComponent implements OnInit, OnChanges {



  private rooms: IRoomLookupModel[];

  public buildingNo: number;

  public dataSource: MatTableDataSource<IRoomLookupModel>;
  public displayedColumns: string[] = ['label', 'actualUse', 'seats', 'facultyLookup'];
  @Input() roomsToDisplay: IRoomLookupModel[];
  @ViewChild(MatSort, { static: true }) sort: MatSort;
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  constructor(
    private readonly roomsService: RoomsService,
    private readonly activeRoute: ActivatedRoute,
    private toastr: ToastrService,
    private router: Router,
    private spinner: NgxSpinnerService
  ) {
    this.dataSource = new MatTableDataSource();
  }
  public applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }
  /**
   * Download the list of rooms for building with number taken from the id route parameter.
   */
  public subscribeRooms() {
    this.activeRoute.paramMap.pipe(
      switchMap(params => {
        // (+) converts string into number
        const id =  params.get('id');
        // get rooms for the specified building
        this.spinner.show();
        if (id) {
          this.buildingNo = +id;
          return this.roomsService.getRooms(this.buildingNo);
        }
        // id is not defined so get all rooms
        return this.roomsService.getRooms();
      })
    ).subscribe(x => {
      this.spinner.hide();
      this.rooms = x.rooms;
      this.dataSource.data = this.rooms;
      if (!x.rooms || x.rooms.length == 0){
        this.toastr.info('Brak dostępnych pomieszczeń dla podanych parametrów 🤯')
      }
    }, () => this.spinner.hide());
  }
  public showRoomDetail(event,roomId){
    event.preventDefault();
    this.router.navigate([`/room/${roomId}`]);
  }
  ngOnInit() {
  // Initialize the component with an input variable or route parameter subscription.
    if(this.roomsToDisplay && this.roomsToDisplay.length > 0)
    {
      this.rooms = this.roomsToDisplay;
      this.dataSource.data = this.rooms;
    }else{
    this.subscribeRooms();
    }
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }
  ngOnChanges(changes: SimpleChanges): void {
   const roomsToDisplayChange: SimpleChange = changes.roomsToDisplay;
   this.rooms = roomsToDisplayChange.currentValue;
   this.dataSource.data = this.rooms;
  }
}
