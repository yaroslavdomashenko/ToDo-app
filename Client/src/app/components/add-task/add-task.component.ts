import { Component, OnInit, Output, Input, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-add-task',
  templateUrl: './add-task.component.html',
  styleUrls: ['./add-task.component.css']
})
export class AddTaskComponent implements OnInit {
  @Input() text:string = "";
  @Output() onCreateTask: EventEmitter<string> = new EventEmitter<string>();

  constructor() { }

  ngOnInit(): void {
  }

  createTask(): void{
    if(this.text == "" ) return;

    this.onCreateTask.emit(this.text);
    this.text = "";
  }

}
