/// <reference path="../../dtos.d.ts" />
import { Component, Input, OnInit, TemplateRef } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';

import { createUrl } from '@servicestack/client';

import { BsModalService } from 'ngx-bootstrap/modal';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';

@Component({
  templateUrl: 'users.component.html',
  styleUrls: ['users.component.scss']
})
export class UsersComponent implements OnInit {
  users: any;
  user: any = {};
  modalRef: BsModalRef;
  config = {
    backdrop: true,
    ignoreBackdropClick: true
  };
  constructor(private route: ActivatedRoute, private http: HttpClient, private modalService: BsModalService) {
    this.getUsers();
  }

  @Input() name: string;

  ngOnInit() {
    this.route.data.subscribe(x => this.name = x.name);
  }

  openModal(template: TemplateRef<any>, user: any) {
    if (user) {
      this.user = JSON.parse(JSON.stringify(user));
    }
    else
      this.user = {status: "Active"};
    this.modalRef = this.modalService.show(template, this.config);
  }

  public trackByFn(index: number, item: any) {
    return item.id;
  }

  getUsers() {
    this.http.get<any>('http://localhost:39324/users').subscribe(r => {
      console.info(r.result);
      if (r && r.result)
        this.users = r.result;
    });
  }

  saveUser() {
    console.info(this.user);
    if (!this.user.id) {
      this.http.post<any>('http://localhost:39324/user', this.user).subscribe(r => {
        if (r && r.result)
          console.info(r);
        this.getUsers();
      });
    } else {
      this.http.put<any>('http://localhost:39324/user', this.user).subscribe(r => {
        if (r && r.result)
          console.info(r);
        this.getUsers();
      });
    }
    this.modalRef.hide();
  }

  deleteUser(user: any) {
    this.http.delete<any>('http://localhost:39324/user/' + user.id).subscribe(r => {
      this.getUsers();
    });
  }
}
