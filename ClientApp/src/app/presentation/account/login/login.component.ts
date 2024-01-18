import { Component, Inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { ILoginUseCaseToken } from '../shared/tokens';
import { ILoginUseCase } from '@domain/models/interfaces/useCases';
import { CatchAll } from '@presentation/shared/decorators';
import { AccountErrorResponse } from '@infrastructure/errors';
import { ILoginRequest } from '@domain/models/DTOs/requests';
import { IAccountLoginEntity } from '@domain/models/interfaces';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent implements OnInit {
  //#region Public Fields
  public loginForm: FormGroup = new FormGroup({});
  public isSubmitted = false;
  public errorMessages: string[] = [];
  //#endregion

  //#region Ctor Dtor
  public constructor(
    @Inject(ILoginUseCaseToken) private readonly loginUseCase: ILoginUseCase,
    private readonly formBuilder: FormBuilder,
    private readonly router: Router
  ) {}
  //#endregion

  //#region Angular LifeCycle Methods
  ngOnInit(): void {
    this.initializeForm();
  }
  //#endregion

  //#region Public Methods
  @CatchAll<LoginComponent>((err, ctx) => {
    ctx.displayErrorMessages(err);
    ctx.isSubmitted = false;
  })
  async login() {
    this.isSubmitted = true;
    this.errorMessages = [];

    const loginRequest: ILoginRequest = this.loginForm.value;

    const loginEntity: IAccountLoginEntity = await this.loginUseCase.execute(
      loginRequest
    );

    this.isSubmitted = false;
  }
  //#endregion

  //#region Private Methods
  private initializeForm(): void {
    this.loginForm = this.formBuilder.group({
      email: ['', [Validators.required]],
      password: ['', [Validators.required]],
    });
  }

  private displayErrorMessages(error: Error) {
    this.errorMessages =
      error instanceof AccountErrorResponse
        ? error.errorMessages
        : [error.message];
  }
  //#endregion
}
