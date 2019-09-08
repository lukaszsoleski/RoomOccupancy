import { Equipment } from 'src/app/models/equipment.model';
import { Component, OnInit, Input } from '@angular/core';
import { RoomsService } from 'src/app/services/rooms.service';

@Component({
  selector: 'app-equipment',
  templateUrl: './equipment.component.html',
  styleUrls: ['./equipment.component.scss']
})
export class EquipmentComponent implements OnInit {

  displayedColumns: string[] = ['position', 'name', 'amount', 'isAvailable'];
  dataSource: Equipment[] = [];

  // tslint:disable-next-line: variable-name
  private _roomId: number;

  constructor(private roomsService: RoomsService) { }
  @Input()
  public set roomId(v: number) {
    this._roomId = v;
    this.getEquipment();
  }
  protected getEquipment() {
     this.roomsService.getEquipment(this._roomId).subscribe(x => {
       this.dataSource = x;
     });
  }

  ngOnInit() {
  }

}
