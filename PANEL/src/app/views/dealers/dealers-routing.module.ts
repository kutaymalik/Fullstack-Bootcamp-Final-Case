import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ListComponent } from './list/list.component';
import { EditComponent } from './edit/edit.component';
import { MessagesComponent } from './messages/messages.component';
import { OrdersComponent } from './orders/orders.component';

const routes: Routes = [
    {
      path: 'list',
      component: ListComponent,
      data: {
        title: 'Dealer List'
      }
    },
    {
      path: 'edit/:id',
      component: EditComponent,
      data: {
        title: 'Dealer Edit'
      }
    },
    {
      path: 'messages/:id',
      component: MessagesComponent,
      data: {
        title: 'Dealer Messages'
      }
    },
    {
      path: 'orders/:id',
      component: OrdersComponent,
      data: {
        title: 'Dealer Orders'
      }
    },
  ];

  @NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
  })
  export class DealersRoutingModule {
  }