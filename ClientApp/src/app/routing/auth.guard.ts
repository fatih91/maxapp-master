import { Injectable } from '@angular/core';
import { Router, CanActivate } from '@angular/router';
import {AccountService} from "../services/account.service";

@Injectable()
export class AuthGuard implements CanActivate {
  constructor(private accountService: AccountService,private router: Router) {}

  canActivate() {

    if(!this.accountService.isLoggedIn())
    {
      this.router.navigate(['/login']);
      return false;
    }
    return true;
  }


}
