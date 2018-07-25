import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { createUrl } from '@servicestack/client';

@Component({
    templateUrl: 'login.component.html',
    styleUrls: ['login.component.scss']
})
export class LoginComponent {
  errorMessage = '';
  userName: string;
  password: string;
  constructor(public router: Router, private route: ActivatedRoute, private http: HttpClient) {
  }

  login() {
    this.errorMessage = "";
    sessionStorage.removeItem("user");
    this.http.post<any>('http://localhost:39324/auth/credentials', { userName: this.userName, password: this.password }).subscribe(r => {
      sessionStorage.setItem('user', JSON.stringify(r));
      this.router.navigate(['/userlist']);
    }, (error: any) => {
        this.errorMessage = "Invalid user name/password. Please try again.";
      });
  }
}
