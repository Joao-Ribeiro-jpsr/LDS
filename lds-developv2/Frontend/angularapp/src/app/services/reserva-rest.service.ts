import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';
import { Reservas } from '../models/Reservas';

const endpoint = 'https://localhost:7115/api/reservas';
const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
};

@Injectable({
  providedIn: 'root'
})

export class ReservaRestService {

  constructor(private http: HttpClient) { }

  criarReserva(newReserva: Reservas): Observable<Reservas> {
    console.log(newReserva);
    return this.http.post<Reservas>(endpoint + '/createreserva', JSON.stringify(newReserva), httpOptions);
  }

  getReservas() {
    return this.http.get(endpoint);
  }

  getReserva(id?: number): Observable<Reservas> {
    return this.http.get<Reservas>(endpoint + '/' + id).pipe(
      catchError(error => {
        console.error('Erro na chamada do serviço:', error);
        return throwError('Erro na chamada do serviço');
      })
    );
  }
  
  getReserveHistory(userId: number) {
    return this.http.get(endpoint + '/reserveHistory/' + userId);
  }
  getPendentReserves(userId: number)  {
    return this.http.get(endpoint + '/pendentReserves/' + userId);
  }
  expirarReserva(reservas: Reservas[]): Observable<Reservas>  {
    return this.http.put(endpoint + '/reservaExpirada/', JSON.stringify(reservas), httpOptions);
  }
  cancelarReserva(reservaId?: number): Observable<Reservas>  {
    return this.http.post<Reservas>(endpoint + '/cancelarreserva/' + reservaId, httpOptions);
  }


}
