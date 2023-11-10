import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListComponent } from './list/list.component';
import { EditComponent } from './edit/edit.component';
import { AvatarModule, ButtonGroupModule, ButtonModule, CardModule, FormModule, GridModule, NavModule, ProgressModule, SpinnerModule, TableModule, TabsModule } from '@coreui/angular';
import { DealersRoutingModule } from './dealers-routing.module';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { MessagesComponent } from './messages/messages.component';
import { OrdersComponent } from './orders/orders.component';
import { ChartjsModule } from '@coreui/angular-chartjs';
import { WidgetsModule } from '../widgets/widgets.module';
import { IconModule } from '@coreui/icons-angular';


@NgModule({
  declarations: [
    ListComponent,
    EditComponent,
    MessagesComponent,
    OrdersComponent
  ],
  imports: [
    CommonModule,
    DealersRoutingModule,
    CardModule,
    TableModule,
    HttpClientModule,
    ButtonModule,
    FormModule,
    ReactiveFormsModule,
    SpinnerModule,
    ChartjsModule,
    TableModule,
    WidgetsModule,
    NavModule,
    IconModule,
    TabsModule,
    GridModule,
    ProgressModule,
    ButtonGroupModule,
    AvatarModule,
  ]
})
export class DealersModule { }
