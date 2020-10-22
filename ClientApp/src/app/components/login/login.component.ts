import { Component } from '@angular/core';
import {Router, ActivatedRoute} from "@angular/router";
import {AccountService} from "../../services/account.service";

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css'],

})
export class LoginComponent {
    constructor(private accountService: AccountService, private router: Router, private route: ActivatedRoute){}

   login(username: string, password: string){
        if(username && password)
          this.accountService.login(username, password).subscribe(res => {
            setTimeout(() =>{
                if(res){
                  const queryParams = this.route.snapshot.queryParams;
                  const redirect = queryParams['redirect'] || '/';
                  this.router.navigateByUrl(decodeURI(redirect));
                }
              },
              1000)
          });
    }

}
