import { ILoginRequest } from '@domain/models/DTOs/requests';
import { IUser } from '@domain/models/interfaces';

export interface ILoginUseCase {
  execute(params: ILoginRequest): Promise<IUser>;
}
