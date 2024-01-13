import { Expose } from 'class-transformer';

import { IBaseErrorResponse } from '@/infrastructure/DTOs/responses/base/base-response.interface';

export class BaseErrorResponse implements IBaseErrorResponse {
  @Expose({ name: 'code' })
  code: number;

  @Expose({ name: 'message' })
  message: string;

  constructor(code: number, message: string) {
    this.code = code;
    this.message = message;
  }
}
