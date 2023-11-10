import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { DealerService } from 'src/app/services/dealer.service';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent implements OnInit {
  data:any[] = []
  loading: boolean = false;

  constructor(
    private dealerService: DealerService,
    private toastr: ToastrService,
    private router: Router){}

  ngOnInit(): void {
    this.load();
  }
  
  load(){
    this.loading = true;
    this.dealerService.getAll().subscribe((response:any) => {
      const responseData = response.response;
      this.data = responseData.$values;
      this.loading = false;
    })
  }
}
