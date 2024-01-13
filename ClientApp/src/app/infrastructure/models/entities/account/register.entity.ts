import { Expose } from 'class-transformer';
import { AccountRegisterEntity } from '@/domain/models/entities';

export class RegisterEntity extends AccountRegisterEntity {
  @Expose({ name: 'title' })
  _title: string;

  @Expose({ name: 'message' })
  _message: string;

  constructor(title: string, message: string) {
    super(title, message);
    this._title = title;
    this._message = message;
  }
}
