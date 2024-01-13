import { IAccountRegisterEntity } from '@domain/models/interfaces';

export class AccountRegisterEntity implements IAccountRegisterEntity {
  //#region public Fields
  title: string;
  message: string;
  //#endregion

  //#region Ctor Dtor
  constructor(title: string, message: string) {
    this.title = title;
    this.message = message;
  }

  //#endregion
}
