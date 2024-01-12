import { IRegisterRequest } from '@/domain/ports/DTOs/requests/account/register';
import { IAccountPort } from '@/domain/ports/account/account-port.interface';

export class AccountUseCase {
  constructor(private readonly accountPort: IAccountPort) {}

  async executeRegister(params: IRegisterRequest): Promise<string> {
    return await this.accountPort.register(params);
  }
}
