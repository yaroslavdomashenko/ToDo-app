import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Task } from '../Models/Task';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
    'Authorization': `Bearer ${localStorage.getItem("token")}`
  })
}; 

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  private apiUrl:string = "https://localhost:5001/api";

  constructor(private http:HttpClient) { }

  getTasks(): Observable<Task[]> {
    const url = `${this.apiUrl}/todo/tasks`;
    return this.http.get<Task[]>(url, httpOptions);
  }

}
