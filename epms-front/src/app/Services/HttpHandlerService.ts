import { Injectable, Injector } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpResponse } from '@angular/common/http';
import { Observable, pipe } from 'rxjs';
import { tap } from 'rxjs/operators';
import { UserContextService } from './user-context.service';


@Injectable()
export class HttpHandlerService implements HttpInterceptor {
  constructor(private user: UserContextService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    this.showLoader();
    return next.handle(req).pipe(tap((event: HttpEvent<any>) => {
      if (event instanceof HttpResponse) {

        this.onEnd();
      }
    },
      (err: any) => {
        this.onEnd();
      }));
  }

  private onEnd(): void {
    this.hideLoader();
  }

  private showLoader(): void {
    this.user.isLoading = true;
  }

  private hideLoader(): void {
    this.user.isLoading = false;
  }
}
