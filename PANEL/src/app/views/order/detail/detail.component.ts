import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { OrderService } from 'src/app/services/order.service';
import { OrderDetail, OrderItem } from '../order-detail.model';

@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.scss']
})
export class DetailComponent implements OnInit {
  data:any
  loading: boolean = false;
  orderId!:number;

  constructor(
    private orderService: OrderService,
    private toastr: ToastrService,
    private router: Router,
    private route: ActivatedRoute){}

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      const id = params['id'];
      this.orderId = +id.replace(':', '');
    })
    this.load();
  }

  load(){
    this.loading = true;
    this.orderService.getById(this.orderId).subscribe({
      next: (data:any) => {
        this.data = data.response;
      },
      error: err => {
        this.toastr.error(err.message, 'Error');
      }
    })
    this.loading = false;
  }
}
