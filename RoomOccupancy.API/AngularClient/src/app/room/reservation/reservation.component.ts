import { ToastrService } from 'ngx-toastr';
import { Component, OnInit } from '@angular/core';
import * as moment from 'moment';
import { FormBuilder, FormGroup, Validators, Form } from '@angular/forms';
@Component({
  selector: 'app-reservation',
  templateUrl: './reservation.component.html',
  styleUrls: ['./reservation.component.scss']
})
export class ReservationComponent implements OnInit {
  public isCollapsed = false;
  protected reservationForm = this.formBuilder.group({
    firstFormGroup : this.formBuilder.group({
      firstCtrl: ['', Validators.required]
    }),
    secondFormGroup : this.formBuilder.group({
      secondCtrl: ['', Validators.required]
    })
  });

  private now = new Date();
  protected selectedEndTime: string;
  protected selectedStartTime: string;
  constructor(private toastr: ToastrService,
              private formBuilder: FormBuilder) {
  }
  ngOnInit() {
    this.selectedEndTime = moment(this.now).add(3, 'hour').format('HH:mm');
    this.selectedStartTime = moment(this.now).add(1, 'hour').format('HH:mm');
  }

}
