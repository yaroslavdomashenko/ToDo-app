import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { Task } from 'src/app/Models/Task';

@Component({
  selector: 'app-task-item',
  templateUrl: './task-item.component.html',
  styleUrls: ['./task-item.component.css']
})
export class TaskItemComponent implements OnInit {
  @Input() task?: Task;

  @Output() onDelete:EventEmitter<Task> = new EventEmitter<Task>();
  @Output() onDoneTask:EventEmitter<Task> = new EventEmitter<Task>();

  constructor() { }

  ngOnInit(): void {
  }

  deleteTask(): void{
    this.onDelete.emit();
  }
  doneTask(): void{
    this.onDoneTask.emit();
  }

}
