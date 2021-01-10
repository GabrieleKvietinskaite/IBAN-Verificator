import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { IIban } from '../models/IIban.interface';
import { map, catchError } from "rxjs/operators";

@Injectable({
  providedIn: 'root'
})
export class IbanService {

  private REST_API_SERVICE = `https://${window.location.host}/api`;
  private httpOptions: any;

  constructor(private httpClient: HttpClient) {
    this.httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    };
  }

  getIban(iban: string){
    let url = `/iban`;
    let body = JSON.stringify(iban)

    return this.httpClient.post<IIban>(this.REST_API_SERVICE + url, body, <Object>this.httpOptions);
  }

  getIbans(ibans: string[]) {
    let url = `/ibans`;
    let body = JSON.stringify(ibans)

    return this.httpClient.post<IIban[]>(this.REST_API_SERVICE + url, body, <Object>this.httpOptions);
  }

  private handleError(error: HttpResponse<Error>) {
    return throwError(error);
  }
}
