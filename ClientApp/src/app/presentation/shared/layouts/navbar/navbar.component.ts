import { Component } from '@angular/core';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css',
})
export class NavbarComponent {
  //#region public Fields
  navBarOpen = false;
  //#endregion

  //#region Public Methods
  toggleNavBar(): void {
    this.navBarOpen = !this.navBarOpen;
  }
  //#endregion
}
