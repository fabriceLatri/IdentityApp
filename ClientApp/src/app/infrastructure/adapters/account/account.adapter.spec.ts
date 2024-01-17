import { TestBed, fakeAsync, tick } from '@angular/core/testing';
import {
  HttpClientTestingModule,
  HttpTestingController,
} from '@angular/common/http/testing';

import { IAccountPort } from '@/domain/ports/interfaces';

import {
  AccountAdapter,
  IAccountPortToken,
} from '@infrastructure/adapters/account/account.adapter';
import type { RegisterDto } from '@domain/models/DTOs/responses';

import { IRegisterRequest } from '@domain/models/DTOs/requests';
import { ApiConstant } from '@/infrastructure/constants/api';
import { RegisterEntity } from '@/infrastructure/models/entities/account/register.entity';
import { IAccountRegisterEntity } from '@/domain/models/interfaces';

describe('Account Adapter implementation suite Tests', () => {
  let httpTestingController: HttpTestingController;
  let accountAdapter: IAccountPort;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [
        {
          provide: IAccountPortToken,
          useClass: AccountAdapter,
        },
      ],
    });

    // Inject the http, test controller, and service-under-test
    // as they will be referenced by each test.
    httpTestingController = TestBed.inject(HttpTestingController);
    accountAdapter = TestBed.inject(IAccountPortToken);
  });

  afterEach(() => {
    httpTestingController.verify();
  });

  describe('Register method', () => {
    const expectedRegisterDto: RegisterDto = {
      code: 201,
      message: 'Your account has been created, you can login',
      title: 'Account Created',
    };
    const accountRegisterEntity: IAccountRegisterEntity = new RegisterEntity(
      expectedRegisterDto.title,
      expectedRegisterDto.message
    );

    it('Should create a new user', fakeAsync(() => {
      const request: IRegisterRequest = {
        firstName: 'John',
        lastName: 'Doe',
        email: 'jdoe@example.com',
        password: '123456',
      };

      let response: IAccountRegisterEntity | undefined;

      // Call the asynchronous method
      accountAdapter.register(request).then((res) => {
        response = res;
      });

      const req = httpTestingController.expectOne(ApiConstant.registerUrl);
      expect(req.request.method).toEqual('POST');

      // Respond with the mock data and complete the observable
      req.flush(expectedRegisterDto);

      // Simulate the passage of time until all pending promises are resolved
      tick();

      // Ensure that the response is as expected
      expect(response).toEqual(accountRegisterEntity);
    }));
  });
});
