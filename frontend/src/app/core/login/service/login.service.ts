import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})

export class LoginService {
  invalidLogin: boolean;

  constructor(private router: Router, private http: HttpClient) { }

  ngOnInit(): void {
  }

  login(form: NgForm) {
    const credentials = {
      'username': form.value.username,
      'password': form.value.password
    }

    this.http.post("https://localhost:44355/api/auth/login", credentials)
    .subscribe(response => {
      const token = (<any>response).token;
      localStorage.setItem("jwt", token);
      localStorage.setItem("currentUser", credentials.username)
      this.invalidLogin = false;
    }, err => {
      this.invalidLogin = true;
    }),
    () => {
    }
  }
}
