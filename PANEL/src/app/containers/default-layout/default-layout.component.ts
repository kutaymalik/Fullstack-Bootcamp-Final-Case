import { Component, OnInit } from '@angular/core';
import { generateNavItems } from './_nav';
import { StorageService } from 'src/app/services/storage.service';
import { INavData } from '@coreui/angular';

@Component({
  selector: 'app-dashboard',
  templateUrl: './default-layout.component.html',
  styleUrls: ['./default-layout.component.scss'],
})
export class DefaultLayoutComponent implements OnInit {

  public Role:any;
  public navItems: INavData[] = [];

  constructor() {}

  ngOnInit(): void {
    this.Role = window.sessionStorage.getItem('role');
    this.navItems = generateNavItems(this.Role);
  }
}
