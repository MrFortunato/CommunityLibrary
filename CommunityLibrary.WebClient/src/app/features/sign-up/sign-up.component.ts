import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormControl ,ReactiveFormsModule, Validators } from '@angular/forms';

import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-sign-up',
  standalone: true,
  imports: [


    ReactiveFormsModule,
    RouterModule],
  templateUrl: './sign-up.component.html',
  styleUrl:  './sign-up.component.scss'
})

export class SignUpComponent {

  signUpForm: FormGroup;

  constructor(private fb: FormBuilder) {
    this.signUpForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(3)]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', [Validators.required]]
    });
  }

  submitSignUp() {
    debugger
    if (this.signUpForm.valid) {
      console.log('Form Data:', this.signUpForm.value);
    } else {
      console.log('Form is invalid');
    }
  }
}
