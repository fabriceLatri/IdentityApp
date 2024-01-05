import { IRegisterRequest } from '@/domain/ports/DTOs/requests/account/register';
import { IAccountPort } from '@/domain/ports/account/account-port.interface';

export class AccountUseCase {
  constructor(private readonly accountPort: IAccountPort) {}

  executeRegister(params: IRegisterRequest): void {
    this.accountPort.register(params);
  }

  getErrorMessages(): string[] {
    return this.accountPort.errorMessages;
  }
}
