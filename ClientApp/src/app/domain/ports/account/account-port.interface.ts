import { IRegisterRequest } from '@/domain/ports/DTOs/requests/account/register';

export interface IAccountPort {
  register(register: IRegisterRequest): Promise<string | never>;
}
