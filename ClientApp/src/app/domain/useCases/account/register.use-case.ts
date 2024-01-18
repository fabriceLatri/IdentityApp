import { IRegisterRequest } from '@domain/models/DTOs/requests';
import { IAccountPort } from '@domain/ports/interfaces';
import { IAccountRegisterEntity } from '@domain/models/interfaces';
import { IRegisterUseCase } from '@domain/models/interfaces/useCases';

export class RegisterUseCase<T> implements IRegisterUseCase {
  constructor(private readonly accountPort: IAccountPort<T>) {}

  async execute(params: IRegisterRequest): Promise<IAccountRegisterEntity> {
    return await this.accountPort.register(params);
  }
}
