import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Users } from '../models/Users';
import { Router } from '@angular/router';

const endpoint = 'https://localhost:7115/api/user';
const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
};

@Injectable({
  providedIn: 'root'
})
export class AuthRestService {

  constructor(private router: Router,private http: HttpClient) { }

  /**
   * Este metodo regista os utilizadores na base de dados
   * @param newUser É o user que é enviado através da API para poder adicionar na base de dados
   * @returns OK se for adicionado com sucesso
   * @return NotFound se o user for null
   */
  registerUser(newUser: Users): Observable<Users> {
    return this.http.post<Users>(endpoint + '/register', JSON.stringify(newUser), httpOptions);
  }

  /**
   * Este metodo permite realizar o login dos utilizadores na aplicação
   * @param email É o email do utilizador
   * @param password É a password do utilizador
   * @returns OK(id) se der login com sucesso. Retorna o id do utilizador
   * @return NotFound se o email ou a password não corresponderem a um user.
   */
  loginUser(email: string, password: string, userId?: string): Observable<AuthRestModelResponse> {
    const loginData = new LoginModel(email, password);
    console.log(loginData)
    if (!userId) {
      return this.http.post<AuthRestModelResponse>(endpoint + '/login', loginData);
    }
    return this.http.post<AuthRestModelResponse>(endpoint + '/login' + userId, loginData);
  }

  /**
   * Este metodo permite retornar um user
   * @param id Atributo que permite selecionar o user
   * @returns OK(user) se for com sucesso
   * @returns NotFound se não exister user
   */
  getUser(id: string): Observable<Users> {
    return this.http.get<Users>(endpoint + '/' + id);

  }

  /**
   * Este metodo permite atualizar os dados do utilizador
   * @param userId Atributo que permite selecionar o user
   * @param updatedUser É o utilizador novo que irá ser alterado na base de dados
   * @returns OK se for com sucesso
   * @returns NotFound, se não existir o user, ou se o novo user for null
   */
  updateUser(userId: number, updatedUser: Users): Observable<Users> {
    return this.http.post<Users>(endpoint + '/editar' + '/' + userId, updatedUser, httpOptions);
  }
}


export class LoginModel {
  constructor(public email: string, public password: string) { }

}


export interface AuthRestModelResponse { }
