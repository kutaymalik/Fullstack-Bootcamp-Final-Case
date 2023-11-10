import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListComponent } from './list/list.component';
import { ButtonModule, CardModule, FormModule, SpinnerModule, TableModule } from '@coreui/angular';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { OrderRoutingModule } from './order-routing.module';
import { CreateOrderComponent } from './create-order/create-order.component';
import { FormsModule } from '@angular/forms';
import { DetailComponent } from './detail/detail.component';
import { PaymentComponent } from './payment/payment.component';
import { PaymentListComponent } from './payment-list/payment-list.component';



@NgModule({
  declarations: [
    CreateOrderComponent,
    ListComponent,
    DetailComponent,
    PaymentComponent,
    PaymentListComponent,
  ],
  imports: [
    CommonModule,
    CardModule,
    TableModule,
    HttpClientModule,
    ButtonModule,
    FormModule,
    ReactiveFormsModule,
    SpinnerModule,
    OrderRoutingModule,
    FormsModule
  ]
})
export class OrderModule { }
