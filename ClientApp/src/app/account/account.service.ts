import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Register } from '@/account/models/register';
import { environment } from 'environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  constructor(private http: HttpClient) {}

  register(model: Register) {
    const registerUrl = `${environment.appUrl}account/register`;
    return this.http.post(registerUrl, model);
  }
}
