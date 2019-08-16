import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from './../../environments/environment';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class HttpClientUtilsService{

  constructor(private http: HttpClient) { }

  public get = <T>(route: string) => {
    return this.http.get<T>(this.createCompleteRoute(route, environment.urlAddress));
  }

  public post = <T>(route: string, body) => {
    return this.http.post<T>(this.createCompleteRoute(route, environment.urlAddress), body, this.generateHeaders());
  }

  public put = <T>(route: string, body) => {
    return this.http.put<T>(this.createCompleteRoute(route, environment.urlAddress), body, this.generateHeaders());
  }

  public delete = (route: string) => {
    return this.http.delete(this.createCompleteRoute(route, environment.urlAddress));
  }

  private createCompleteRoute = (route: string, envAddress: string) => {
    return `${envAddress}/${route}`;
  }

  private generateHeaders = () => {
    return {
      headers: new HttpHeaders({'Content-Type': 'application/json'})
    }
  }
}
