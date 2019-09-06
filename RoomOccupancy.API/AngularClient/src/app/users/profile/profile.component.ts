import { UserProfileModel } from './../../models/users/user-profile.model';
import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { UsersService } from 'src/app/services/users.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

  protected profile = new UserProfileModel();

  constructor(private spinner: NgxSpinnerService,
              private usersService: UsersService) {}

  public getProfile() {
    this.spinner.show();
    this.usersService.getProfile().subscribe(x => {
      this.spinner.hide();
      this.profile = x;
    }, () => this.spinner.hide());
  }
  ngOnInit() {
     this.getProfile();
  }

}
