import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { OrderService } from 'src/app/services/order.service';
import { PaymentService } from 'src/app/services/payment.service';

@Component({
  selector: 'app-payment-list',
  templateUrl: './payment-list.component.html',
  styleUrls: ['./payment-list.component.scss']
})
export class PaymentListComponent {
  data:any
  loading: boolean = false;

  constructor(
    private paymentService: PaymentService,
    private toastr: ToastrService,
    private router: Router,
    private route: ActivatedRoute){}

  ngOnInit(): void {
    this.load();
  }

  load(){
    this.loading = true;
    this.paymentService.getDealer().subscribe((response:any) => {
      const responseData = response.response;
      this.data = responseData.$values;
      this.loading = false;
    })
  }
}
