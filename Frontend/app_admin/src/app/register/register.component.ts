import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from "@angular/forms";
import { Router } from '@angular/router';
import { AuthenticationService } from '../services/authentication.service';
import { User } from '../models/user';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent implements OnInit {
  public formError: string = '';
  submitted = false;

  // variables bound to form fields in the template
  credentials = {
    name: '',
    email: '',
    password: ''
  }

  // takes the router and auth service
  constructor(
    private router: Router,
    private authenticationService: AuthenticationService
  ) { }

  ngOnInit(): void {}

  // fires on register button
  public onRegisterSubmit() : void {
    this.formError = '';
    // check form fields
    if (!this.credentials.email || !this.credentials.name || 
      !this.credentials.password) {
        // set form error for any missing fields
        this.formError = 'All fields are required, please try again';
        this.router.navigateByUrl('#'); // return to register page
    } else {
      // register the new user
      this.doRegister();
    }
  }

  // registers the new user
  private doRegister() : void {
    let newUser = {
      name: this.credentials.name,
      email: this.credentials.email
    } as User

    this.authenticationService.register(newUser, this.credentials.password);

    // auth service should log the user in if registration successful
    if(this.authenticationService.isRegistered())
    {
      // console.log('Router::Direct');
      this.router.navigate(['/login']);
    } else {
      var timer = setTimeout(() => {
        if(this.authenticationService.isRegistered())
        {
          // console.log('Router::Pause');
          this.router.navigate(['/login']);
        }},3000);
    }
  }
}
