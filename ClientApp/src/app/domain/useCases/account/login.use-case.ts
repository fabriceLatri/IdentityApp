import { ILoginRequest } from '@domain/models/DTOs/requests';
import { IAccountLoginEntity } from '@domain/models/interfaces';
import { ILoginUseCase } from '@domain/models/interfaces/useCases';
import { IAccountPort } from '@domain/ports/interfaces';

export class LoginUseCase implements ILoginUseCase {
  //#region Ctor, Dtor
  constructor(private readonly accountPort: IAccountPort) {}
  //#endregion

  //#region Public Methods
  async execute(params: ILoginRequest): Promise<IAccountLoginEntity> {
    return this.accountPort.login(params);
  }
  //#endregion
}
