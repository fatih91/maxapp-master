import {Component, OnInit} from '@angular/core';
import {Router} from "@angular/router";
import {AccountService} from "../../services/account.service";

@Component({
    selector: 'max-nav-menu',
    templateUrl: './navmenu.component.html',
    styleUrls: ['./navmenu.component.scss']
})
export class NavMenuComponent{
    constructor(private router:Router, private accountService:AccountService){}

    logout(){
      console.log("Logout");
      this.accountService.logout();
      this.router.navigate(['/login']);
    }
}
