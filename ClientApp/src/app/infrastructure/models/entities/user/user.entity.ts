import { IUser } from '@domain/models/interfaces';
import { Expose } from 'class-transformer';

export class User implements IUser {
  @Expose()
  firstName: string;

  @Expose()
  lastName: string;

  @Expose()
  token: string;

  constructor(firstName: string, lastName: string, token: string) {
    this.firstName = firstName;
    this.lastName = lastName;
    this.token = token;
  }
}
