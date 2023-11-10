import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListComponent } from './list/list.component';
import { AccordionModule, ButtonModule, CardModule, FormModule, SpinnerModule, TableModule } from '@coreui/angular';
import { AdminOrderRoutingModule } from './admin-order-routing.module';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { OrderDetailComponent } from './order-detail/order-detail.component';
import { PaymentDetailsComponent } from './payment-details/payment-details.component';


@NgModule({
  declarations: [
    ListComponent,
    OrderDetailComponent,
    PaymentDetailsComponent
  ],
  imports: [
    CommonModule,
    AdminOrderRoutingModule,
    CardModule,
    TableModule,
    HttpClientModule,
    ButtonModule,
    FormModule,
    ReactiveFormsModule,
    SpinnerModule,
    AccordionModule
  ]
})
export class AdminOrderModule { }
