import { ILoginRequest } from '@domain/models/DTOs/requests';
import { IAccountLoginEntity } from '@domain/models/interfaces';

export interface ILoginUseCase {
  execute(params: ILoginRequest): Promise<IAccountLoginEntity>;
}
