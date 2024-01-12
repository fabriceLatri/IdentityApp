import { Expose } from 'class-transformer';

export class RegisterResponse {
  @Expose()
  code: number;
  @Expose()
  message: string;

  constructor(message: string, code: number) {
    this.message = message;
    this.code = code;
  }
}
