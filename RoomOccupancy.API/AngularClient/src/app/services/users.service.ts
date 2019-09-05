import { Router } from '@angular/router';
import { Observable, BehaviorSubject } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpClientUtilsService } from '../common/http-client-utils.service';
import { map, tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  public isLoggedIn: BehaviorSubject<boolean>;
  private set setToken(v: string) {
    localStorage.setItem('auth_token', v);
  }
  private get hasToken() {
    return localStorage.getItem('auth_token') != 'null';
  }
  constructor(private httpService: HttpClientUtilsService, private router: Router) {
    this.isLoggedIn = new BehaviorSubject<boolean>(this.hasToken);
   }

  public login(userName: string, password: string) {
    console.log({ userName, password });
    return this.httpService.post('login', { userName, password })
      .pipe(
        tap(x => {
          const token = Object.assign({auth_token: ''}, x);
          this.setToken = token.auth_token;
          this.isLoggedIn.next(true);
        })
      );
  }
  public register(form: any) {
    return this.httpService.post('register', form);
  }
  public logout() {
    localStorage.setItem('auth_token', null);
    this.isLoggedIn.next(false);
    this.router.navigate([`/campus`]);
  }
}
