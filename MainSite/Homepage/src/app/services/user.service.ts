import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpResponse } from '@angular/common/http';
import { Token } from '../models/Token';
import { Observable, catchError, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private httpClient: HttpClient) { }

  login(username: string, password: string) : Observable<HttpResponse<String> | HttpErrorResponse>{
    return this.httpClient.post('api/auth/login', {username, password}, {observe: 'response', responseType: 'text'})
    .pipe(catchError((error: HttpErrorResponse) => of(error)));
  }

}
