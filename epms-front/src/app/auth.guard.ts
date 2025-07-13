import {Injectable} from '@angular/core';
import {
  CanActivate,
  CanActivateChild,
  CanLoad,
  Route,
  UrlSegment,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  UrlTree, Router
} from '@angular/router';
import {Observable} from 'rxjs';
import {AuthorizationService} from "./Services/Authorization/authorization.service";
import {UserContextService} from "./Services/user-context.service";
import {map} from "rxjs/operators";

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate, CanActivateChild, CanLoad {


  constructor(private authorizationService: AuthorizationService,
              private user: UserContextService, private router: Router) {
  }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {

    console.log("next", next);
    return true;
  /*  return this.authorizationService
      .LoadAuthorizedMenus(
        this.user.Username,
        this.user.CompanyID,
        "HRMS",
        "HOBJ"
      ).pipe(
        map((permissions: any) => {
          console.log("permissions", permissions);
          console.log("permissions");
          if (permissions.IsError === false) {
            return false;
          } else {
            let result = permissions.Data.filter(a => {
              return a.URL.replace(new RegExp('\\b' + "#/" + '\\b', 'gi'), '') == next.routeConfig.path;
            }).length > 0;

            if (result === false) {
             // this.router.push('forbidden');

            }
            return result;
          }
        })
      );*/
  }

  canActivateChild(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    return true;
  }

  canLoad(
    route: Route,
    segments: UrlSegment[]): Observable<boolean> | Promise<boolean> | boolean {
    return true;
  }
}
