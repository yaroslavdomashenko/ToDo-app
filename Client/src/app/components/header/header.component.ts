import { Component, Input, OnInit } from '@angular/core';
import { global } from '../common/global';


@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  isLoggined:boolean = false;

  constructor() { }

  ngOnInit(): void {
    this.isLoggined = global.isLoggined;
  }



}
