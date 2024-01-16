import { IAccountRegisterEntity } from '@domain/models/interfaces';

export class AccountRegisterEntity implements IAccountRegisterEntity {
  //#region public Fields
  _title: string;
  _message: string;
  //#endregion

  //#region Ctor Dtor
  constructor(title: string, message: string) {
    this._title = title;
    this._message = message;
  }
  //#endregion

  //#region Accessors
  get title() {
    return this._title;
  }

  get message() {
    return this._message;
  }
  //#endregion
}
