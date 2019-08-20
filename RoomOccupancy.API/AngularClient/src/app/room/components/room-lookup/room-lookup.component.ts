import { IRoomLookupModel, RoomListViewModel } from './../../../models/campus/room-lookup.model';
import { RoomsService } from './../../../services/rooms.service';
import { Component, OnInit, ViewChild, Output } from '@angular/core';
import { switchMap } from 'rxjs/operators';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { MatTableDataSource, MatSort, MatPaginator } from '@angular/material';
@Component({
  selector: 'app-room-lookup',
  templateUrl: './room-lookup.component.html',
  styleUrls: ['./room-lookup.component.scss']
})
export class RoomLookupComponent implements OnInit {

  private rooms: IRoomLookupModel[];

  public buildingNo: number;

  public dataSource: MatTableDataSource<IRoomLookupModel>;
  public displayedColumns: string[] = ['label', 'actualUse', 'seats', 'facultyLookup'];

  @ViewChild(MatSort, { static: true }) sort: MatSort;
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  constructor(
    private readonly roomsService: RoomsService,
    private readonly route: ActivatedRoute
  ) {
    this.dataSource = new MatTableDataSource();
  }
  public applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }
  /**
   * Download the list of rooms for building with number taken from the id route parameter.
   */
  public GetBuildingRooms() {
    this.route.paramMap.pipe(
      switchMap(params => {
        // (+) converts string into number
        const id =  params.get('id');
        // get rooms for the specified building
        if (id) {
          this.buildingNo = +id;
          return this.roomsService.getRooms(this.buildingNo);
        }
        // id is not defined so get all rooms
        return this.roomsService.getRooms();
      })
    ).subscribe(x => {
      this.rooms = x.rooms;
      this.dataSource.data = this.rooms;
      console.log(`GetBuildingRooms call with ${this.buildingNo} parameter returned ${this.rooms.length} elements.`);
    });
  }

  ngOnInit() {
    this.GetBuildingRooms();
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

}
