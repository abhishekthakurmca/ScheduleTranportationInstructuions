import { Component } from '@angular/core';
import { ClientList } from './@shared/Constants/const'
import * as THREE from 'three';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {


  title = 'my-angular-app';
  events: Array<string> = [];
  EmployeesList: any[] = [];

  ngOnInit() {
    this.EmployeesList = ClientList;
  }

  logEvent(eventName:any) {
    this.events.unshift(eventName);
  }

  clearEvents() {
    this.events = [];
  }
}

