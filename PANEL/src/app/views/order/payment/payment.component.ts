import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { OrderService } from 'src/app/services/order.service';
import { CardService } from 'src/app/services/card.service';
import { PaymentService } from 'src/app/services/payment.service';
import { OrderDetail, OrderItem } from '../order-detail.model';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.scss']
})
export class PaymentComponent {
  cardData:any
  filteredCards: any[] = [];
  orderData:any
  loading: boolean = false;
  orderId!:number;
  selectedCard: any;

  paymentForm = new FormGroup({
    paymentType: new FormControl('EftPayment'),
    paymentDescription: new FormControl(''),
    cardNumber: new FormControl(''),
    orderId: new FormControl(0)
  })

  constructor(
    private fb: FormBuilder,
    private orderService: OrderService,
    private cardService: CardService,
    private paymentService: PaymentService,
    private toastr: ToastrService,
    private router: Router,
    private route: ActivatedRoute){}

    ngOnInit(): void {
      this.route.params.subscribe((params) => {
        const id = params['id'];
        this.orderId = +id.replace(':', '');
        console.log(this.orderId);
      })
      this.load();
      
    }

    load(){
      this.loading = true;
      this.paymentForm.controls.orderId.setValue(this.orderId);
      this.cardService.get().subscribe((response:any) => {
        const responseData = response.response;
        this.cardData = responseData.$values;
        // this.onPaymentTypeChange();
        this.loading = false;
      })

      this.orderService.getById(this.orderId).subscribe({
        next: (data:any) => {
          this.orderData = data.response;
        },
        error: err => {
          this.toastr.error(err.message, 'Error');
        }
      })
      this.loading = false;
    }

    onChooseCard(cardNumber: string, card:any) {
      this.paymentForm.get('cardNumber')?.setValue(cardNumber);
      this.selectedCard = card;
      // this.paymentForm.controls.cardId.setValue(cardId);
      console.log(cardNumber)
    }

    onSubmit(){
      console.log(this.paymentForm.value)
      this.paymentService.create(this.paymentForm.value).subscribe({
        next: (data:any) => {
          if(data.success == false){
            console.log(data);
            this.toastr.error(data.message, 'Error')
          } 
          else {
            this.toastr.info('Payment was made successfully.', data.message);
            console.log(data)
            this.router.navigate(['order/list'])
          }
        },
        error: err => {
          this.toastr.error(err.error.error, 'Error')
        }
      })
    }
}
