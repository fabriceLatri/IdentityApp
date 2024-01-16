import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegisterComponent } from './register.component';
import { Router } from '@angular/router';
import { FormBuilder } from '@angular/forms';
import { AccountUseCase } from '@/domain/useCases/account/account.use-case';
import { SharedServiceToken } from '@/presentation/shared/services/injectionToken';
import { SharedService } from '@/presentation/shared/services/shared.service';
import { IAccountPort } from '@/domain/ports/account/account-port.interface';
import {
  IAccountPortToken,
  AccountAdapter,
} from '@/infrastructure/adapters/account/account.adapter';
import { BsModalService } from 'ngx-bootstrap/modal';

describe('RegisterComponent', () => {
  let component: RegisterComponent;
  let fixture: ComponentFixture<RegisterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [RegisterComponent],
      providers: [
        Router,
        FormBuilder,
        { provide: IAccountPortToken, useClass: AccountAdapter },
        BsModalService,
        {
          provide: AccountUseCase,
          useFactory: (accountAdapter: IAccountPort) =>
            new AccountUseCase(accountAdapter),
          deps: [IAccountPortToken],
        },
        {
          provide: SharedServiceToken,
          useClass: SharedService,
        },
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(RegisterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
