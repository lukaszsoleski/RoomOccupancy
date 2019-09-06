import { Router } from '@angular/router';
import { Observable, BehaviorSubject } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpClientUtilsService } from '../common/http-client-utils.service';
import { map, tap } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';
import { UserProfileModel } from '../models/users/user-profile.model';
@Injectable({
  providedIn: 'root'
})
export class UsersService {

  public isLoggedIn: BehaviorSubject<boolean>;

  public get isAuthenticated() {
    if (!this.hasToken) {
      return false;
    }
    return !this.isTokeExpired();
  }

  private set setToken(v: string) {
    localStorage.setItem('auth_token', v);
  }
  public get getToken() {
    return localStorage.getItem('auth_token');
  }
  public get hasToken() {
    return localStorage.getItem('auth_token') !== 'null';
  }
  constructor(private httpService: HttpClientUtilsService, private router: Router) {
    this.isLoggedIn = new BehaviorSubject<boolean>(this.isAuthenticated);
  }
  public getProfile(){
    return this.httpService.get<UserProfileModel>('user');
  }
  public login(userName: string, password: string) {
    return this.httpService.post('login', { userName, password })
      .pipe(
        tap(x => {
          const token = Object.assign({ auth_token: '' }, x);
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
  private isTokeExpired() {
    const helper = new JwtHelperService();
    const token = this.getToken;
    const isExpired = helper.isTokenExpired(token);
    return isExpired;
  }

}
