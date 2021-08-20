import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  username:string = "";
  password:string = "";

  constructor() { }

  ngOnInit(): void {
  }

  onSubmit(): void{
    console.log("Username: " + this.username + "\nPassword: " + this.password);

    this.password = '';
    this.username = '';
  }

}
