import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormControl } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { MessageService } from 'src/app/services/message.service';
import { SignalRService } from 'src/app/services/signal-r.service';
import { Subscription, interval } from 'rxjs';
import { ChangeDetectorRef } from '@angular/core';
import { switchMap } from 'rxjs/operators';


@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.scss']
})
export class MessagesComponent implements OnInit,  OnDestroy {
  
  data:any[] = []
  loading: boolean = false;
  dealerId!:number;
  sessionId: string | null = window.sessionStorage.getItem('id');
  sessionRole: string | null = window.sessionStorage.getItem('role');

  private dataRefreshSubscription!: Subscription;
  messages: (string | { content: string; receiverId: string | null; role: string |null } | null)[] = [];
  private messageSubscription!: Subscription;

  messageForm = new FormGroup({
    receiverId: new FormControl(''), // Bu alana alıcı (dealer veya admin) ID'sini girmeniz gerekir
    content: new FormControl('')
  });

  constructor(
    private messageService: MessageService,
    private signalRService: SignalRService,
    private toastr: ToastrService,
    private router: Router,
    private route: ActivatedRoute,
    private cdr: ChangeDetectorRef,){}

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      const id = params['id'];
      this.dealerId = +id.replace(':', '');
    })
    this.load();

    this.messageSubscription = this.signalRService.messageReceived$.subscribe(message => {
      this.messages.push(message);
      this.cdr.detectChanges(); 
    });

    this.dataRefreshSubscription = interval(2000) // 2000 milliseconds = 2 seconds
      .pipe(
        switchMap(() => this.messageService.getDealer(Number(this.sessionId)))
      )
      .subscribe((response: any) => {
        const responseData = response.response;

        this.data = [];
        this.messages = [];
        this.data = responseData.$values;
        const contentArray = this.data.map(item => item.content);
        this.messages.push(
          ...this.data.map(item => ({
            content: item.content,
            receiverId: item.receiverId,
            role: item.role
          }))
        );
        this.loading = false;
      });
  }

  isMessageObject(message: any): message is { content: string; receiverId: string; role: string } {
    return typeof message === 'object' && message !== null && 'content' in message && 'receiverId' in message && 'role' in message;
  }

  ngOnDestroy(): void {
    this.messageSubscription.unsubscribe();
  }

  sendMessage() {
    const receiverId = this.dealerId.toString();
    const content = this.messageForm.value.content;

    const safeContent = content || '';
    
    // SignalR servisine mesaj gönder
    
    try {
      if (this.sessionId !== null && this.sessionRole !== null) {
        this.signalRService.sendMessage(this.sessionId, receiverId, safeContent, this.sessionRole);
      } else {
        console.log('Session ID null olduğu için SendMessage fonksiyonu çağrılamadı.');
      }
    } catch (error: any) {
      console.log('SendMessage fonksiyonunda bir hata oluştu:', error.message);
    }
    
  
    // Gönderilen mesajı yerel listeye ekle
    this.messages.push({ content: safeContent, receiverId: receiverId, role: this.sessionRole });

    this.messageForm.reset();
  
  }

  load(){
    this.loading = true;
    this.messageService.getDealer(this.dealerId).subscribe((response:any) => {
      const responseData = response.response;
      this.data = responseData.$values;
      console.log(this.data)
      const contentArray = this.data.map(item => item.content);
      this.messages.push(...this.data.map(item => ({
        content: item.content,
        receiverId: item.receiverId,
        role: item.role
      })));
      this.loading = false;
    })
  }
}
