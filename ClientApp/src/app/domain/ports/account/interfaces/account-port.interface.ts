import { IAccountRegisterEntity } from '@domain/models/interfaces';
import { IRegisterRequest } from '@domain/models/DTOs/requests';

export interface IAccountPort {
  register(register: IRegisterRequest): Promise<IAccountRegisterEntity>;
}
