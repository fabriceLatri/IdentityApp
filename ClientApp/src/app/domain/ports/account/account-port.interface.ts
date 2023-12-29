import { Observable } from 'rxjs';
import { IRegisterRequest } from '@/domain/ports/account/DTOs/requests/register';
import { IRegisterResponse } from '@/domain/ports/account/DTOs/responses/register';

export interface IAccountPort {
  register(register: IRegisterRequest): Observable<IRegisterResponse>;
}
