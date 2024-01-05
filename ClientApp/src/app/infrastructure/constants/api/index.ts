import { environment } from 'environments/environment.development';

export class ApiConstant {
  static readonly registerUrl = `${environment.appUrl}account/register`;
}
