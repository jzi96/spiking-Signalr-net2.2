import { Component, OnInit, Inject } from '@angular/core';
import * as signalR from '@aspnet/signalr';

@Component({
  selector: 'app-position-view',
  templateUrl: './position-view.component.html',
  styleUrls: ['./position-view.component.css']
})
export class PositionViewComponent implements OnInit {

  private _hubConnection: signalR.HubConnection;
  public _positions: Position
  constructor(@Inject('BASE_URL') private baseUrl: string) { }
  //https://docs.microsoft.com/de-de/aspnet/core/tutorials/signalr-typescript-webpack?view=aspnetcore-2.2&tabs=visual-studio



  ngOnInit() {
    this._hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(this.baseUrl + 'hubs/positions')
      .build();
    this._hubConnection.on('OnPositionUpdated', (position: Position) => {
      this._positions = position;
    });
    this._hubConnection.start().catch(err => document.write(err));

  }

}
export interface Position {
  grid: string;
  values: Array<SimpleItem>;
}

export  interface SimpleItem{
  key: Date;
  value: number;
}
