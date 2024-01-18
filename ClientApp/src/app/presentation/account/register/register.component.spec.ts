import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegisterComponent } from './register.component';
import { Router } from '@angular/router';
import { FormBuilder } from '@angular/forms';
import { BsModalService, ModalModule } from 'ngx-bootstrap/modal';
import { HttpClientModule } from '@angular/common/http';
import { IAccountPort } from '@domain/ports/interfaces';
import { RegisterUseCase } from '@domain/useCases';
import { AccountAdapter } from '@infrastructure/adapters/account/account.adapter';
import { IAccountPortToken } from '@presentation/shared/injectionTokens';
import { SharedServiceToken } from '@presentation/shared/services/injectionToken';
import { SharedService } from '@presentation/shared/services/shared.service';
import { SharedModule } from '@presentation/shared/shared.module';
import { Observable } from 'rxjs';
import { IUser } from '@domain/models/interfaces';

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
          useFactory: (
            accountAdapter: IAccountPort<Observable<IUser | null>>
          ) => new RegisterUseCase(accountAdapter),
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
