import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';

import { ClassToggleService, HeaderComponent } from '@coreui/angular';
import { AuthService } from 'src/app/services/auth.service';
import { StorageService } from 'src/app/services/storage.service';

@Component({
  selector: 'app-default-header',
  templateUrl: './default-header.component.html',
})
export class DefaultHeaderComponent extends HeaderComponent implements OnInit {

  @Input() sidebarId: string = "sidebar";
  sessionRole: string | null = window.sessionStorage.getItem('role');
  sessionId: number | null = Number(window.sessionStorage.getItem('id'));
  public newMessages = new Array(4)
  public newTasks = new Array(5)
  public newNotifications = new Array(5)
  user:any;

  constructor(
    private classToggler: ClassToggleService, 
    private storage:StorageService,
    private auth: AuthService) {
    super();
  }

  ngOnInit(): void {
    this.user = this.storage.getUser();
  }

  signOut(){
    this.auth.logOut();
  }
}
