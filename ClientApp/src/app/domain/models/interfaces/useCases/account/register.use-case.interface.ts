import { IRegisterRequest } from '@domain/models/DTOs/requests';
import { IAccountRegisterEntity } from '@domain/models/interfaces';

export interface IRegisterUseCase {
  execute(params: IRegisterRequest): Promise<IAccountRegisterEntity>;
}
