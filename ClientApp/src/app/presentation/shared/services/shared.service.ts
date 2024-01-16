import { Injectable } from '@angular/core';

import { BsModalRef, BsModalService, ModalOptions } from 'ngx-bootstrap/modal';

import { ISharedService } from '@presentation/shared/services/interfaces';
import { NotificationComponent } from '../components/modals/notification/notification.component';

@Injectable({
  providedIn: 'root',
})
export class SharedService implements ISharedService {
  //#region Public Fields
  bsModalRef?: BsModalRef<NotificationComponent>;
  //#endregion
  constructor(private readonly modalService: BsModalService) {}

  showNotification(isSuccess: boolean, title: string, message: string): void {
    const initialState: ModalOptions = {
      initialState: {
        isSuccess,
        title,
        message,
      },
      class: 'modal-dialog-centered',
    };

    this.bsModalRef = this.modalService.show(
      NotificationComponent,
      initialState
    );
  }
}
