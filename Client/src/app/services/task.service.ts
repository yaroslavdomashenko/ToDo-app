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

  doneTask(id:number): Observable<Task>{
    const url = `${this.apiUrl}/todo/change-status`;
    const ChangeModel = {
      id: id
    };
    return this.http.post<Task>(url, ChangeModel, httpOptions);
  }

  createTask(text:string): Observable<Task>{
    const url = `${this.apiUrl}/todo/create`;

    const TaskModel = {
      text: text
    };
    return this.http.post<Task>(url, TaskModel, httpOptions);
  }

  deleteTask(id:number): Observable<Task>{
    const url = `${this.apiUrl}/todo/delete/${id}`;
    return this.http.delete<Task>(url, httpOptions);
  }

  updateTask(id:number, text:string){
    const url = `${this.apiUrl}/todo/update`;
    const TaskUpdateModel = {
      id: id,
      text: text
    };
    return this.http.put<Task>(url, TaskUpdateModel, httpOptions);
  }

}
