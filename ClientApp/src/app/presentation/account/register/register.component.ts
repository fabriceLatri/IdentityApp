import { Component, Inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AccountUseCase } from '@domain/useCases/account/account.use-case';
import { IRegisterRequest } from '@/infrastructure/DTOs/requests/account/register';
import { CatchAll } from '@/presentation/shared/decorators';
import { AccountErrorResponse } from '@/infrastructure/errors';
import { IAccountRegisterEntity } from '@/domain/models/interfaces';
import { ISharedService } from '@/presentation/shared/services/interfaces';
import { SharedServiceToken } from '@/presentation/shared/services/injectionToken';

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
    private readonly accountUseCase: AccountUseCase,
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
          Validators.pattern('^\\w+@[a-zA-Z_]+?\\.[a-zA-Z]{2,3}$'),
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

  // @CatchAll<RegisterComponent>((err, instance) => {
  //   instance.displayErrorMessages(err);
  // })

  @CatchAll<RegisterComponent>((err, ctx) => {
    ctx.displayErrorMessages(err);
  })
  async register() {
    this.isSubmitted = true;
    this.errorMessages = [];

    // if (this.registerForm.valid) {
    const registerRequest: IRegisterRequest = this.registerForm.value;
    const registerEntity: IAccountRegisterEntity =
      await this.accountUseCase.executeRegister(registerRequest);

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
