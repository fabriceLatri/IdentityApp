import { IAccountLoginEntity } from '@domain/models/interfaces';

export class AccountLoginEntity implements IAccountLoginEntity {
  //#region Protected Fields
  protected _firstName: string;

  protected _lastName: string;
  //#endregion

  //#region Ctor, Dtor
  constructor(firstName: string, lastName: string) {
    this._firstName = firstName;
    this._lastName = lastName;
  }
  //#endregion

  //#region Public Accessors
  public get firstName(): string {
    return this._firstName;
  }

  public get lasttName(): string {
    return this._lastName;
  }
  //#endregion
}
