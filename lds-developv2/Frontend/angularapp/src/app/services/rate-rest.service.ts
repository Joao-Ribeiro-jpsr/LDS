import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Reservas } from '../models/Reservas';
import { Observable } from 'rxjs/internal/Observable';
import { catchError } from 'rxjs/internal/operators/catchError';
import { map, of, throwError } from 'rxjs';
import { Rating } from '../models/Rating';

const endpoint = 'https://localhost:7115/api/rating/';
const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
};

@Injectable({
  providedIn: 'root'
})
export class RateRestService {

  constructor(private http: HttpClient) { }

  createRate(rating: Rating): Observable<Rating> {
    console.log(rating);
    return this.http.post<Rating>(endpoint + 'rate/' , JSON.stringify(rating), httpOptions);
  }

  getRatings(recintoid?: number): Observable<Rating> {
    return this.http.get<Rating>(endpoint + recintoid);
  }

  

}
