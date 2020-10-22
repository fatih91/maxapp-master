import {RouterModule, Routes} from "@angular/router";
import {homeRoutes, homeRoutingComponents} from "./home.routing";
import {AuthGuard} from "./auth.guard";
import {AlreadyLoggedGuard} from "./already-logged.guard";
import {LoginComponent} from "../components/login/login.component";

export const appRoutes: Routes = [
    {path: '', canActivate: [AuthGuard], children: homeRoutes},
    {path: 'login', canActivate: [AlreadyLoggedGuard], component: LoginComponent},
    {path: '**', redirectTo: ''}
];

export const appRouting = RouterModule.forRoot(appRoutes, {useHash: true});

export const routingComponents = [
    ...homeRoutingComponents
];
