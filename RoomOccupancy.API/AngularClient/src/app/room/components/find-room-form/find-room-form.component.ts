import { Faculty } from './../../../models/faculty.model';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { RoomsService } from './../../../services/rooms.service';
import { Equipment } from 'src/app/models/equipment.model';
import { Component, OnInit, ViewChild } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { forkJoin, Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { MatSelect } from '@angular/material';

@Component({
  selector: 'app-find-room-form',
  templateUrl: './find-room-form.component.html',
  styleUrls: ['./find-room-form.component.scss']
})
export class FindRoomFormComponent implements OnInit {
  protected equipment: Equipment[] = [];
  protected faculties: Faculty[] = [];
  protected roomTypes: string [];

  protected findRoomForm: FormGroup;
  protected roomFilterCtrl = new FormControl();

  @ViewChild('singleSelect', {static: false}) singleSelect: MatSelect;
  /** Subject that emits when the component has been destroyed. */
  protected _onDestroy = new Subject<void>();


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
      facultyId: [''],
      seats: [0],
      equipment: [[]],
      roomType: ['']
    });
  }

  public filterRoomTypes(){

    let keyword = this.roomFilterCtrl.value;
    if(!keyword){
      this
      return;
    }

  }
  ngOnInit() {
    this.loadFormData();
    this.buildForm();

    this.roomFilterCtrl.valueChanges
      .pipe(takeUntil(this._onDestroy))
      .subscribe(() => {
        this.filterRoomTypes();
      });
  }

}
