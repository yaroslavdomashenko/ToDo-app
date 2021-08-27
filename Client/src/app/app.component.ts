import { Component } from '@angular/core';
import { global } from '../app/components/common/global';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Simple todo';

  ngOnInit() {
    if(localStorage.getItem("token") != null){
      global.isLoggined = true;
    }
  }

}
