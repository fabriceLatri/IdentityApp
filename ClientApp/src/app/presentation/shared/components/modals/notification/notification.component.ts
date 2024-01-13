import { Component } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-notification',
  templateUrl: './notification.component.html',
  styleUrl: './notification.component.css',
})
export class NotificationComponent {
  //#region public Fields
  isSuccess = true;
  title = '';
  message = '';
  //#endregion

  //#region Ctor Dtor
  constructor(public bsModalRef: BsModalRef) {}
  //#endregion
}
