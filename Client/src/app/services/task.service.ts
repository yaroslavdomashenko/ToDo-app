import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Task } from '../Models/Task';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
    'authorization': `Bearer ${localStorage.getItem("token")}`
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
    console.log(httpOptions.headers);
    return this.http.get<Task[]>(url, httpOptions);
  }

  doneTask(id:number): Observable<Task>{
    const url = `${this.apiUrl}/todo/change-status/${id}`;
    return this.http.post<Task>(url, httpOptions);
  }

  createTask(text:string): Observable<Task>{
    const url = `${this.apiUrl}/todo/create`;

    const TaskModel = {
      text: text
    };

    console.log(httpOptions.headers);
    return this.http.post<Task>(url, TaskModel, httpOptions);
  }

}
