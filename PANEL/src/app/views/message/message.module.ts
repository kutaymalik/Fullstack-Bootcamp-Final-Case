import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MessageComponent } from './message/message.component';
import { SignalRService } from '../../services/signal-r.service';
import { MessageRoutingModule } from './message-routing.module';
import { ReactiveFormsModule } from '@angular/forms';
import { ButtonModule, CardModule, FormModule, SpinnerModule, TableModule } from '@coreui/angular';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [MessageComponent],
  imports: [
    CommonModule,
    CardModule,
    TableModule,
    HttpClientModule,
    ButtonModule,
    FormModule,
    ReactiveFormsModule,
    SpinnerModule,
    MessageRoutingModule
  ],
  providers: [SignalRService],
  exports: [MessageComponent], // İhtiyaca bağlı olarak bileşenin dışa aktarılması
})
export class MessageModule {}
