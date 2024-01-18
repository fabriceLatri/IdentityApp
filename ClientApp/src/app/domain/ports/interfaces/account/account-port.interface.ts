import { IUser, IAccountRegisterEntity } from '@domain/models/interfaces';
import { ILoginRequest, IRegisterRequest } from '@domain/models/DTOs/requests';

export interface IAccountPort<T> {
  register(register: IRegisterRequest): Promise<IAccountRegisterEntity>;
  login(login: ILoginRequest): Promise<IUser>;
  user: T;
}
