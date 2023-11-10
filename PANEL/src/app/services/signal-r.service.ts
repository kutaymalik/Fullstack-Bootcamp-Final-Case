import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import { Observable, Subject } from 'rxjs';
import { environment } from '../../environments/environment';
import { HttpHeaders } from '@angular/common/http';

const httpOptions = {
  headers : new HttpHeaders({'Content-Type' : 'application/json'})
}

@Injectable({
  providedIn: 'root',
})
export class SignalRService {
  private AUTH_API = environment.AUTH_API;
  private hubConnection!: HubConnection;
  private messageReceivedSubject = new Subject<string>();

  messageReceived$: Observable<string> = this.messageReceivedSubject.asObservable();

  constructor() {
    this.createConnection();
    this.registerOnServerEvents();
    this.startConnection();
  }

  private createConnection() {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl("https://localhost:7088/messageHub") // MessageHub'ın bulunduğu URL'yi buraya ekleyin
      .build();
  }

  private startConnection() {
    this.hubConnection
      .start()
      .then(() => {
        console.log('Connection started');
      })
      .catch((err) => {
        console.error('Error while starting connection: ' + err);
      });
  }

  private registerOnServerEvents(): void {
    this.hubConnection.on('ReceiveMessage', (senderId: string, content: string) => {
      this.messageReceivedSubject.next(`${senderId}: ${content}`);
    });
  }

  sendMessage(senderId: string ,receiverId: string, content: string, role: string): void {
    this.hubConnection.invoke('SendMessage', senderId, receiverId, content, role);
  }
}
