import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { UsersComponent } from './users/users.component';
import { LoginComponent } from './login/login.component';

import { JsonServiceClient } from '@servicestack/client';
import { AuthGuard } from './auth/auth.guard';

import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AddHeaderInterceptor } from './interceptor/addheader.interceptor';
import { ModalModule } from 'ngx-bootstrap';

export const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'userlist', component: UsersComponent, canActivate: [AuthGuard] },
  { path: 'login', component: LoginComponent }
];

@NgModule({
  declarations: [
    AppComponent,
    UsersComponent,
    LoginComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    RouterModule.forRoot(routes),
    ModalModule.forRoot()
  ],
  providers: [
    AuthGuard,
    { provide: JsonServiceClient, useValue: new JsonServiceClient('/') },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AddHeaderInterceptor,
      multi: true,
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
