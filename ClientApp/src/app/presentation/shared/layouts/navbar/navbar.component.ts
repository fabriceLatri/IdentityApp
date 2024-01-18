import { Component, Inject } from '@angular/core';
import { IUser } from '@domain/models/interfaces';
import { IAccountPort } from '@domain/ports/interfaces';
import { IAccountPortToken } from '@presentation/shared/injectionTokens';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css',
})
export class NavbarComponent {
  //#region public Fields
  navBarOpen = false;
  //#endregion

  //#region Ctor, Dtor
  public constructor(
    @Inject(IAccountPortToken)
    private readonly _accountPort: IAccountPort<Observable<IUser | null>>
  ) {}
  //#endregion

  //#region Getters Accessors
  public get user$(): Observable<IUser | null> {
    return this._accountPort.user;
  }
  //#endregion

  //#region Public Methods
  toggleNavBar(): void {
    this.navBarOpen = !this.navBarOpen;
  }

  logout(): void {}
  //#endregion
}
