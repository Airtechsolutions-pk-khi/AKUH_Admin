import { Injectable } from '@angular/core';
import * as signalR from "@microsoft/signalr"
import { ToastService } from 'src/app/_services/toastservice';
import { Notification } from '../_models/Notifications';
import { async } from '@angular/core/testing';
import { AlertService } from '../_alert/alert.service';

@Injectable({
  providedIn: 'root'
})
export class SignalrService {
  options = {
    autoClose: false,
    keepAfterRouteChange: false
  };
  private hubConnection: signalR.HubConnection;
  constructor(
    public ts: ToastService,
    public alertService: AlertService) {
  }

  public async startConnection() {
    
    //var domain = "http://localhost:59660";
     var domain = "http://admin.mamjihospital.online";
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(`${domain}/Notify`).build();

    await this.hubConnection.start();

    this.hubConnection.on('privateMessageMethodName', (data) => {
      
      console.log('private Message:' + data);
    });

    this.hubConnection.on('publicMessageMethodName', (data: Notification) => {
      
      console.log('public Message:' + data.message);
      this.ts.showToast(data.title, data.message);
      // this.ts.showWarning(data.title, data.message);
      // this.alertService.success('Success!!', this.options)
      // alert(data.message);
    });

    this.hubConnection.on('clientMethodName', (data) => {
      
      console.log('server message:' + data);
    });

    this.hubConnection.on('WelcomeMethodName', (data) => {
      
      console.log('client Id:' + data);
      this.hubConnection.invoke('GetDataFromClient', 'abc@abc.com', data).catch(err => console.log(err));
    });
  }
}
