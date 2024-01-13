import { IRegisterRequest } from '@/infrastructure/DTOs/requests/account/register';
import { IAccountPort } from '@/domain/ports/account/account-port.interface';
import { IAccountRegisterEntity } from '@/domain/models/interfaces';

export class AccountUseCase {
  constructor(private readonly accountPort: IAccountPort) {}

  async executeRegister(
    params: IRegisterRequest
  ): Promise<IAccountRegisterEntity> {
    return await this.accountPort.register(params);
  }
}
