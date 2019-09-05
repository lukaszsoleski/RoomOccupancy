import { UsersService } from 'src/app/services/users.service';
import { Component, OnInit } from '@angular/core';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Observable } from 'rxjs';
import { map, share } from 'rxjs/operators';

@Component({
  selector: 'app-main-nav',
  templateUrl: './main-nav.component.html',
  styleUrls: ['./main-nav.component.scss']
})
export class MainNavComponent implements OnInit {

  public isLoggedIn = false;
  isHandset$: Observable<boolean> = this.breakpointObserver.observe(Breakpoints.Handset)
    .pipe(
      map(result => result.matches),
      share()
    );

  constructor(private breakpointObserver: BreakpointObserver,
              private usersService: UsersService) { }

  protected logout() {
    this.usersService.logout();
  }
  ngOnInit(): void {
    this.usersService.isLoggedIn.subscribe(x => {
      this.isLoggedIn = x;
    });
  }
}
