import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { NgForm } from '@angular/forms';
import { LoginService } from 'src/app/core/login/service/login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  invalidLogin: boolean;

  constructor(private router: Router, private loginService: LoginService ) { }

  ngOnInit(): void {
  }

  navigate(url){
    this.router.navigateByUrl(url);
  }
  login(form: NgForm) {
    this.loginService.login(form);
    this.router.navigateByUrl("/");
  }
}
