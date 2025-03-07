import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RecintoFakeAPI } from '../models/RecintoFakeAPI';



const endpoint = 'https://fakeapifriendly.azurewebsites.net/api/recintos/';
const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
};

@Injectable({
  providedIn: 'root'
})
export class FakeApiRestService {

  constructor(private http: HttpClient) { }

   /**
   *
   * Este metodo retorna um recinto em especifico da base de dados
   * @param id Id necessário para escolher o recinto
   * @returns OK se existir o recinto
   * @returns NotFound se não existir esse recinto
   */
  getRecinto(id?: number): Observable<RecintoFakeAPI> {
    return this.http.get<RecintoFakeAPI>(endpoint + id);
  }

}
