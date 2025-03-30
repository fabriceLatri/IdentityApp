import { environment } from 'environments/environment.development';

export class ApiConstant {
  static readonly registerUrl = `${environment.appUrl}auth/register`;
  static readonly loginUrl = `${environment.appUrl}account/login`;
}
