import {Router} from "@angular/router"
import { Component, OnInit, Output } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { global } from "../common/global";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  username:string = "";
  password:string = "";
  
  constructor(private authService:AuthService, private router:Router) { }

  ngOnInit(): void {
  }

  onSubmit(): void{
    const newModel = {
      username: this.username,
      password: this.password
    };

    this.authService.Login(newModel).subscribe(
      (data) => this.AuthUser(data.data),
      (error) => alert("Wrong password")
    );
    
    global.isLoggined = !global.isLoggined;
    console.log(global.isLoggined);

    this.password = '';
    this.username = '';
  }

  private AuthUser(token:string): void{
    localStorage.setItem("token", token);
    document.location.href = "/";
  }

}
