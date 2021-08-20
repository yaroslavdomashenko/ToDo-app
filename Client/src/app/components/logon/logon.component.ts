import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-logon',
  templateUrl: './logon.component.html',
  styleUrls: ['./logon.component.css']
})
export class LogonComponent implements OnInit {
  username:string = "";
  password:string = "";
  confirmPassword = "";
  modelError:boolean = false;

  constructor() { }

  ngOnInit(): void {
  }

  onSubmit(): void{
    console.log("Username: " + this.username + "\nPassword: " + this.password + "\nConfirm: " + this.confirmPassword);

    if(this.confirmPassword !== this.password){
      alert("Confirm password")
      return;
    }

    this.password = '';
    this.username = '';
    this.confirmPassword = '';
  }

}
