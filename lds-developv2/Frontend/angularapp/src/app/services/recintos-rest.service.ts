import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Recintos } from '../models/Recintos';

const endpoint = 'https://localhost:7115/api/recintos/';
const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
};

@Injectable({
  providedIn: 'root'
})
export class RecintosRestService {

  constructor(private http: HttpClient) { }

  /**
   *
   * Este metodo retorna todos os recintos da base de dados
   * @returns OK se existirem recintos
   * @returns NotFound se não existir nenhum na base de dados
   */
  getRecintos() {
    return this.http.get(endpoint);
  }

  /**
   *
   * Este metodo retorna um recinto em especifico da base de dados
   * @param id Id necessário para escolher o recinto
   * @returns OK se existir o recinto
   * @returns NotFound se não existir esse recinto
   */
  getRecinto(id?: number): Observable<Recintos> {
    return this.http.get<Recintos>(endpoint + id);
  }

}
