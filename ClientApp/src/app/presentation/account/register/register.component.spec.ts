import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegisterComponent } from './register.component';
import { Router } from '@angular/router';
import { FormBuilder } from '@angular/forms';
import { RegisterUseCase } from '@/domain/useCases/account/register.use-case';
import { SharedServiceToken } from '@/presentation/shared/services/injectionToken';
import { SharedService } from '@/presentation/shared/services/shared.service';
import { IAccountPort } from '@/domain/ports/interfaces';
import {
  IAccountPortToken,
  AccountAdapter,
} from '@/infrastructure/adapters/account/account.adapter';
import { BsModalService, ModalModule } from 'ngx-bootstrap/modal';
import { HttpClientModule } from '@angular/common/http';
import { SharedModule } from '@/presentation/shared/shared.module';

describe('RegisterComponent', () => {
  let component: RegisterComponent;
  let fixture: ComponentFixture<RegisterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [RegisterComponent],
      imports: [HttpClientModule, SharedModule, ModalModule.forRoot()],
      providers: [
        Router,
        FormBuilder,
        { provide: IAccountPortToken, useClass: AccountAdapter },
        BsModalService,
        {
          provide: RegisterUseCase,
          useFactory: (accountAdapter: IAccountPort) =>
            new RegisterUseCase(accountAdapter),
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
