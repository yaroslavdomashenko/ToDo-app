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
    this.taskSevice.getTasks().subscribe((tasks) => this.tasks = tasks);
  }

  doneTask(task:Task){
    task.status = !task.status;
  }

  deteleTask(task:Task){
    console.log(task);
  }

}
