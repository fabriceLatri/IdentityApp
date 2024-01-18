import { ILoginRequest } from '@domain/models/DTOs/requests';
import { IUser } from '@domain/models/interfaces';
import { ILoginUseCase } from '@domain/models/interfaces/useCases';
import { IAccountPort } from '@domain/ports/interfaces';

export class LoginUseCase<T> implements ILoginUseCase {
  //#region Ctor, Dtor
  constructor(private readonly accountPort: IAccountPort<T>) {}
  //#endregion

  //#region Public Methods
  async execute(params: ILoginRequest): Promise<IUser> {
    return this.accountPort.login(params);
  }
  //#endregion
}
