import { Injectable } from '@angular/core';
import { HttpClientUtilsService } from '../common/http-client-utils.service';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  constructor(private httpService: HttpClientUtilsService) { }

  public login(userName: string, password: string) {
   return this.httpService.post('api/login', {userName, password});
  }
}
