import { Faculty } from './../../../models/faculty.model';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { RoomsService } from './../../../services/rooms.service';
import { Equipment } from 'src/app/models/equipment.model';
import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-find-room-form',
  templateUrl: './find-room-form.component.html',
  styleUrls: ['./find-room-form.component.scss']
})
export class FindRoomFormComponent implements OnInit {

  protected form: FormGroup;
  public get f() {
    return this.form.controls;
  }
  protected equipment: Equipment[] = [];
  protected faculties: Faculty[] = [];
  constructor(private roomsService: RoomsService,
              private spinner: NgxSpinnerService,
              private toastr: ToastrService,
              private fb: FormBuilder) { }
  private loadFormData() {
    //TODO: forkJoin
  }
  private buildForm() {
    this.form = this.fb.group({
      facultyId: [''],
      equipment: [[]],
      seats: [''],

    });
  }
  ngOnInit() {
    this.loadFormData();
  }

}
