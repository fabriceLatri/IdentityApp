import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AccountUseCase } from '@domain/useCases/account/account.use-case';
import { IRegisterRequest } from '@/domain/ports/DTOs/requests/account/register';
import { CatchAll } from '@/presentation/shared/decorators';
import { AccountErrorResponse } from '@/infrastructure/errors';

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
    private readonly accountUseCase: AccountUseCase,
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
    await this.accountUseCase.executeRegister(registerRequest);
  }

  private displayErrorMessages(error: Error) {
    this.errorMessages =
      error instanceof AccountErrorResponse
        ? error.errorMessages
        : [error.message];
  }
}
