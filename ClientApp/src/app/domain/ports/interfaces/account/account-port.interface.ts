import {
  IAccountLoginEntity,
  IAccountRegisterEntity,
} from '@domain/models/interfaces';
import { ILoginRequest, IRegisterRequest } from '@domain/models/DTOs/requests';

export interface IAccountPort {
  register(register: IRegisterRequest): Promise<IAccountRegisterEntity>;
  login(login: ILoginRequest): Promise<IAccountLoginEntity>;
}
