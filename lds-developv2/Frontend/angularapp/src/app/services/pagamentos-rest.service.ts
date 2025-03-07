import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Reservas } from '../models/Reservas';
import { Observable } from 'rxjs/internal/Observable';
import { catchError } from 'rxjs/internal/operators/catchError';
import { map, of, throwError } from 'rxjs';
import { Pagamento } from '../models/Pagamentos';

const endpoint = 'https://localhost:7115/api/pagamento/';
const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
};

@Injectable({
  providedIn: 'root'
})
export class PagamentosRestService {

  constructor(private http: HttpClient) { }
  /**
   *
   * Este metodo utiliza a api para adicionar pagamentos na base de dados
   * @param id é o id da reserva onde vai ser adicionado o pagamento  
   * @param newPagamento o pagamento que irá ser enviado
   * @returns OK caso seja adicionado com sucesso
   * @returns NotFound caso ocorra um erro ao adicionar ou caso não existe o pagamento enviado
   */
  createPagamento(id:number, newPagamento: Pagamento,pontos:number): Observable<Pagamento> {
    console.log(newPagamento);

    return this.http.post<Pagamento>(endpoint + 'createpagamento/' + id + '/' + pontos, JSON.stringify(newPagamento), httpOptions);
  }
}
