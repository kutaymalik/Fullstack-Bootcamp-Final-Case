import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ListComponent } from './list/list.component';
import { OrderDetailComponent } from './order-detail/order-detail.component';
import { PaymentDetailsComponent } from './payment-details/payment-details.component';

const routes: Routes = [
    {
      path: 'list',
      component: ListComponent,
      data: {
        title: 'Order List'
      }
    },
    {
      path: 'detail/:id',
      component: OrderDetailComponent,
      data: {
        title: 'Order List'
      }
    },
    {
      path: 'payment-details/:id',
      component: PaymentDetailsComponent,
      data: {
        title: 'Order List'
      }
    },
  ];
  
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})

  export class AdminOrderRoutingModule {
}