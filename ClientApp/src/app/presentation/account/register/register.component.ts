import { Component, Inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { IRegisterUseCase } from '@domain/models/interfaces/useCases';
import { IRegisterRequest } from '@domain/models/DTOs/requests/account/register';
import { IAccountRegisterEntity } from '@domain/models/interfaces';

import { AccountErrorResponse } from '@infrastructure/errors';
import { ValidatorHelper } from '@infrastructure/validators/constants';

import { IRegisterUseCaseToken } from '@presentation/account/shared/tokens';
import { CatchAll } from '@presentation/shared/decorators';
import { ISharedService } from '@presentation/shared/services/interfaces';
import { SharedServiceToken } from '@presentation/shared/services/injectionToken';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup = new FormGroup({});
  isSubmitted = false;
  errorMessages: string[] = [];

  constructor(
    @Inject(SharedServiceToken) private readonly sharedService: ISharedService,
    @Inject(IRegisterUseCaseToken)
    private readonly registerUseCase: IRegisterUseCase,
    private readonly router: Router,
    private formBuilder: FormBuilder
  ) {}

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm() {
    this.registerForm = this.formBuilder.group({
      firstName: [
        '',
        [
          Validators.required,
          Validators.minLength(3),
          Validators.maxLength(15),
        ],
      ],
      lastName: [
        '',
        [
          Validators.required,
          Validators.minLength(3),
          Validators.maxLength(15),
        ],
      ],
      email: [
        '',
        [
          Validators.required,
          Validators.pattern(ValidatorHelper.VALIDATE_EMAIL_PATTERN),
        ],
      ],
      password: [
        '',
        [
          Validators.required,
          Validators.minLength(6),
          Validators.maxLength(15),
        ],
      ],
    });
  }

  @CatchAll<RegisterComponent>((err, ctx) => {
    ctx.displayErrorMessages(err);
    ctx.isSubmitted = false;
  })
  async register() {
    this.isSubmitted = true;
    this.errorMessages = [];

    const registerRequest: IRegisterRequest = this.registerForm.value;
    const registerEntity: IAccountRegisterEntity =
      await this.registerUseCase.execute(registerRequest);

    this.sharedService.showNotification(
      true,
      registerEntity.title,
      registerEntity.message
    );

    this.router.navigateByUrl('/account/login');
  }

  private displayErrorMessages(error: Error) {
    this.errorMessages =
      error instanceof AccountErrorResponse
        ? error.errorMessages
        : [error.message];
  }
}
