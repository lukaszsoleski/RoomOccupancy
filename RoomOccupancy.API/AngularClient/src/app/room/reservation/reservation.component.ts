import { Reservation } from './../../models/schedule/reservation';
import { DaysOfWeekPipe } from './../../common/pipes/DaysOfWeekPipe';

import { ScheduleLookupModel } from './../../models/schedule/schedule-lookup';
import { ToastrService } from 'ngx-toastr';
import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import * as moment from 'moment';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DatePipe } from '@angular/common';
import { WeekDays } from 'src/app/models/WeekDays';
@Component({
  selector: 'app-reservation',
  templateUrl: './reservation.component.html',
  styleUrls: ['./reservation.component.scss']
})
export class ReservationComponent implements OnInit {
  protected reservationForm: FormGroup;

  private now = new Date();
  protected defaultEndTime: string;
  protected defaultStartTime: string;
  // get names
  protected daysOfWeek = Object.values(WeekDays).filter(x => x.length > 1);
  @Output() reservationTime = new EventEmitter<Reservation>();

  constructor(private toastr: ToastrService,
              private formBuilder: FormBuilder,
              private datepipe: DatePipe,
              private daysOfWeekPipe: DaysOfWeekPipe) {
  }
  private initForm() {
    const dayName = this.daysOfWeekPipe.transform(moment(new Date()).isoWeekday());
    this.reservationForm = this.formBuilder.group({
      topic: this.formBuilder.group({
        subject: ['', Validators.required]
      }),
      timeframe: this.formBuilder.group({
        start: [this.defaultStartTime, Validators.required],
        end: [this.defaultEndTime, Validators.required],
        day: [new Date(), Validators.required],
      }),
      repeat: this.formBuilder.group({
        isCyclical: [false],
        weekDays: [[dayName]],
        reservationEnd: [new Date()]
      })
    });
    this.reservationForm.get('timeframe').get('day').valueChanges.subscribe(value => {
      const day = this.daysOfWeekPipe.transform(moment(value).isoWeekday());
      this.reservationForm.get('repeat').patchValue({ weekDays: [day] });
    });
  }
  ngOnInit() {
    this.defaultEndTime = moment(this.now).add(3, 'hour').set('minutes', 0).format('HH:mm');
    this.defaultStartTime = moment(this.now).add(1, 'hour').set('minutes', 0).format('HH:mm');
    this.initForm();
  }
  protected publishForm() {
    const formdata = Object.assign({}, this.reservationForm.value);
    const model = new Reservation();
      // The day the reservation is ended depends on whether it is repeatable or not.
    const endDay = this.getFormEndDay(formdata);

    model.subject = formdata.topic.subject;
    model.start = this.getDateTime(formdata.timeframe.start, formdata.timeframe.day);
    model.end = this.getDateTime(formdata.timeframe.end, endDay);
    model.isCyclical = formdata.repeat.isCyclical;

    for (const day of formdata.repeat.weekDays) {
      const val = parseInt(WeekDays[day], 10);
      if (isNaN(val)) { continue; }
      model.reservationDays.push(val);
    }

    this.reservationTime.emit(model);
  }

  // The day the reservation is ended depends on whether it is repeatable or not.
  private getFormEndDay(formdata: any) {
    let day;
    if (formdata.repeat.isCyclical === true) {
     day = formdata.repeat.reservationEnd;
    } else {
     day = formdata.timeframe.day;
    }
    return day;
  }

  private getDateTime(time: string, day: Date): Date {
    const timeArr = time.split(':');
    const hours = parseInt(timeArr[0], 10);
    const minutes = parseInt(timeArr[1], 10);

    const date = moment(day).set('hours', hours).set('minutes', minutes).toDate();
    console.log(date.toISOString());
    return date;
  }
}
