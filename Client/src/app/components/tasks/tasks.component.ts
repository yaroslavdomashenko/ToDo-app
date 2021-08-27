import { Component, OnInit } from '@angular/core';
import { Task } from 'src/app/Models/Task';
import { TaskService } from 'src/app/services/task.service';

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.css']
})
export class TasksComponent implements OnInit {
  tasks: Task[] = [];

  constructor(private taskSevice:TaskService) { }

  ngOnInit(): void {
    this.taskSevice.getTasks().subscribe(
      tasks => this.tasks = tasks, 
      err => alert("Server error")
    );
    
  }

  createTask(text:string): void{
    this.taskSevice.createTask(text).subscribe((task) => this.tasks.push(task));
  }

  doneTask(task:Task): void{
    this.taskSevice.doneTask(task.id).subscribe(
      () => task.status = !task.status
    );
  }

  deteleTask(task:Task): void{
    this.taskSevice.deleteTask(task.id).subscribe(
      () => this.tasks = this.tasks.filter(x => x.id != task.id)
    );
  }

  updateTask(task:Task, newText:string): void{
    this.taskSevice.updateTask(task.id, newText).subscribe( (updatedTask) => this.ChangeText(updatedTask));
  }

  private ChangeText(updatedTask:Task){
    this.tasks.forEach(task => {
      if(task.id == updatedTask.id) task.text = updatedTask.text;
    })
  }

}
