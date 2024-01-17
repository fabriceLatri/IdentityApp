import { Expose } from 'class-transformer';
import { AccountLoginEntity } from '@domain/models/entities';

export class LoginEntity extends AccountLoginEntity {
  @Expose({ name: 'firstName' })
  override _firstName: string;

  @Expose({ name: 'lastName' })
  override _lastName: string;

  constructor(firstName: string, lastName: string) {
    super(firstName, lastName);
    this._firstName = firstName;
    this._lastName = lastName;
  }

  //#region Accessors
  override get firstName() {
    return this._firstName;
  }

  override get lasttName() {
    return this._lastName;
  }
  //#endregion
}
