import { IFindRoomParameters } from './../../../models/campus/find-room-params';
import { Faculty } from './../../../models/faculty.model';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { RoomsService } from './../../../services/rooms.service';
import { Equipment } from 'src/app/models/equipment.model';
import { Component, OnInit, ViewChild, Output, EventEmitter } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { forkJoin, Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { MatSelect } from '@angular/material';
import { IRoomLookupModel } from 'src/app/models/campus/room-lookup.model';

@Component({
  selector: 'app-find-room-form',
  templateUrl: './find-room-form.component.html',
  styleUrls: ['./find-room-form.component.scss']
})
export class FindRoomFormComponent implements OnInit {
  protected equipment: Equipment[] = [];
  protected faculties: Faculty[] = [];
  protected roomTypes: string[];

  protected findRoomForm: FormGroup;

  @Output() rooms: EventEmitter<IRoomLookupModel[]> = new EventEmitter();

  public get f() {
    return this.findRoomForm.controls;
  }

  constructor(private roomsService: RoomsService,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService,
    private fb: FormBuilder) { }

  public loadFormData() {
    this.spinner.show();
    forkJoin(
      this.roomsService.getFaculties(),
      this.roomsService.getEquipment(),
      this.roomsService.getRoomTypes()
    ).subscribe(([f, e, r]) => {
      this.faculties = f;
      this.equipment = e;
      this.roomTypes = r;
      this.spinner.hide();
    }, () => this.spinner.hide());
  }
  public buildForm() {
    this.findRoomForm = this.fb.group({
      facultyId: [],
      seats: [15, [Validators.min(0), Validators.max(500)]],
      selectedEquipment: [],
      roomType: []
    });
  }
  protected onSubmit() {
    if (this.findRoomForm.invalid) {
      return;
    }
    const model : IFindRoomParameters = {
      equipment: this.f.selectedEquipment.value,
      faculty: this.f.facultyId.value,
      roomName: this.f.roomType.value,
      seats: this.f.seats.value
    }
    this.spinner.show();
    this.roomsService.findRoom(model).subscribe(x => {
      this.spinner.hide();
      if(x.noResultMessage){
        this.toastr.warning(x.noResultMessage);
      }else{
        this.rooms.emit(x.rooms);
      }
        this.toastr.info(`Znaleziono ${x.rooms.length} pomieszczenia.`);

    }, () => this.spinner.hide());
  }
  ngOnInit() {
    this.loadFormData();
    this.buildForm();

  }
}
