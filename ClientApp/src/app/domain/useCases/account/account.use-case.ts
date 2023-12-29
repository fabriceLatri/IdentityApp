import { Observable } from 'rxjs';

import { IRegisterRequest } from '@/domain/ports/account/DTOs/requests/register';
import { IRegisterResponse } from '@/domain/ports/account/DTOs/responses/register';
import { IAccountPort } from '@/domain/ports/account/account-port.interface';
import { IUseCase } from '@domain/useCases/base/use-case.interface';

export class AccountUseCase
  implements IUseCase<IRegisterRequest, IRegisterResponse>
{
  constructor(private readonly accountPort: IAccountPort) {}

  execute(params: IRegisterRequest): Observable<IRegisterResponse> {
    return this.accountPort.register(params);
  }
}
