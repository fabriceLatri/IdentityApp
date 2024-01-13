import { IAccountRegisterEntity } from '@/domain/models/interfaces';
import { IRegisterRequest } from '@/infrastructure/DTOs/requests/account/register';

export interface IAccountPort {
  register(register: IRegisterRequest): Promise<IAccountRegisterEntity>;
}
