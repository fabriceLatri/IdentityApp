import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IRegisterRequest } from '@/domain/ports/account/DTOs/requests/register';
import { environment } from 'environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  constructor(private http: HttpClient) {}

  register(model: IRegisterRequest) {
    const registerUrl = `${environment.appUrl}account/register`;
    return this.http.post(registerUrl, model);
  }
}
