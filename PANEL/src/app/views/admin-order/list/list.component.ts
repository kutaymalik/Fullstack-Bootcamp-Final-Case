import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { OrderService } from 'src/app/services/order.service';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent implements OnInit {
  data:any[] = []
  loading: boolean = false;

  constructor(
    private orderService: OrderService,
    private toastr: ToastrService,
    private router: Router){}

  ngOnInit(): void {
    this.load();
  }

  load(){
    this.loading = true;
    this.orderService.getAll().subscribe((response:any) => {
      const responseData = response.response;
      this.data = responseData.$values;
      this.loading = false;
    })
  }

  confirmOrder(id:any){
    if(confirm('Are you sure you want to confirm this order?')){
      this.orderService.confirm(id).subscribe({
        next: (response: any) => {
          this.toastr.info('Order Confirmed', response.message);

          this.load();
        }
      })
    }
  }

  cancelOrder(id:any){
    if(confirm('Are you sure you want to cancel this order?')){
      this.orderService.cancel(id).subscribe({
        next: (response: any) => {
          this.toastr.info('Order Cancelled', response.message);
          this.load();
        }
      })
    }
  }

  ngOnDestroy(): void {
    
  }
}
