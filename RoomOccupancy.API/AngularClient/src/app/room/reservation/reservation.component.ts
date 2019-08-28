import { ToastrService } from 'ngx-toastr';
import { Component, OnInit } from '@angular/core';
import * as moment from 'moment';
@Component({
  selector: 'app-reservation',
  templateUrl: './reservation.component.html',
  styleUrls: ['./reservation.component.scss']
})
export class ReservationComponent implements OnInit {

  private now = new Date();
  protected selectedEndTime: string;
  protected selectedStartTime : string;
  constructor(private toastr: ToastrService) {
  }

  ngOnInit() {
    this.selectedEndTime = moment(this.now).add(3, 'hour').format('HH:mm');
    this.selectedStartTime = moment(this.now).add(1, 'hour').format('HH:mm');
  }

}
