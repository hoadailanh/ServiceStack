'use strict';
import { Injectable, Injector } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class AddHeaderInterceptor implements HttpInterceptor {
  constructor(private injector: Injector) { }
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    // Clone the request to add the new header
    var headers = req.headers.set('Accept', 'application/json');
    headers.set('Content-Type', 'application/json');
    req = req.clone({ headers: headers });
    req = req.clone({ withCredentials: true });

    return next.handle(req);
  }
}
