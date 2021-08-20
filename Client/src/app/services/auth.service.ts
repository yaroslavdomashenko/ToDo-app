import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { loginModel } from '../Models/loginModel';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
}; 

@Injectable({
  providedIn: 'root'
})

export class AuthService {
  private apiUrl:string = "https://localhost:5001/api";

  constructor(private http:HttpClient) { }

  Login(model:loginModel): Observable<any>{
    const url:string = `${this.apiUrl}/authorization/login`;
    return this.http.post(url, model, httpOptions);
  }

}
