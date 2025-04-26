import { Expose } from 'class-transformer';
import { AccountRegisterEntity } from '@domain/models/entities';

export class RegisterEntity extends AccountRegisterEntity {
  @Expose({ name: 'title' })
  override _title: string;

  @Expose({ name: 'message' })
  override _message: string;

  constructor(title: string, message: string) {
    super(title, message);
    this._title = title;
    this._message = message;
  }

  //#region Accessors
  override get title() {
    return this._title;
  }

  override get message() {
    return this._message;
  }
  //#endregion
}
