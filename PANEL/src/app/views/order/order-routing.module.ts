import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateOrderComponent } from './create-order/create-order.component';
import { ListComponent } from './list/list.component';
import { DetailComponent } from './detail/detail.component';
import { PaymentComponent } from './payment/payment.component';
import { PaymentListComponent } from './payment-list/payment-list.component';

const routes: Routes = [
  {
    path: 'list',
    component: ListComponent,
    data: {
      title: 'Order List'
    }
  },
  {
    path: 'createorder',
    component: CreateOrderComponent,
    data: {
      title: 'Create Order'
    }
  },
  {
    path: 'detail/:id',
    component: DetailComponent,
    data: {
      title: 'Order Detail'
    }
  },
  {
    path: 'payment/:id',
    component: PaymentComponent,
    data: {
      title: 'Order Payment'
    }
  },
  {
    path: 'payment-list',
    component: PaymentListComponent,
    data: {
      title: 'Payment List'
    }
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class OrderRoutingModule {
}
