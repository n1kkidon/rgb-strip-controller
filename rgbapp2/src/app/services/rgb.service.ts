import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams, HttpResponse } from '@angular/common/http';

import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class RgbService {

  constructor(private http: HttpClient) { 
  }
  
  getLedStatus(){
    return this.http.get('/rgb/getLedState').pipe();
  }
}
