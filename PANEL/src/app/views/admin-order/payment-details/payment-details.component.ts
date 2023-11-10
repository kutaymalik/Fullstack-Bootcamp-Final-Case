import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { PaymentService } from 'src/app/services/payment.service';

@Component({
  selector: 'app-payment-details',
  templateUrl: './payment-details.component.html',
  styleUrls: ['./payment-details.component.scss']
})
export class PaymentDetailsComponent implements OnInit {
  data:any
  loading: boolean = false;
  orderId!:number;

  constructor(
    private paymentService: PaymentService,
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
      this.paymentService.getByOrderId(this.orderId).subscribe({
        next: (data:any) => {
          this.data = data.response;
          console.log('payment: ' + this.data)
        },
        error: err => {
          this.toastr.error(err.message, 'Error');
        }
      })
      this.loading = false;
    }
}
