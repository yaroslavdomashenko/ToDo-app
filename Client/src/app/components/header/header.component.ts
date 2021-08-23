import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { global } from '../common/global';


@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  isLoggined:boolean = false;

  constructor(private router:Router) { }

  ngOnInit(): void {
    this.isLoggined = global.isLoggined;
  }

  logOut(): void{
    localStorage.removeItem("token");
    document.location.href = "/";
  }

}
