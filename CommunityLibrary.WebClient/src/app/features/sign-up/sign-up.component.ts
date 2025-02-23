import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FormControl, FormGroup,ReactiveFormsModule,Validators } from '@angular/forms';

@Component({
  selector: 'app-sign-up',
  imports: [RouterModule, ReactiveFormsModule],
  templateUrl: './sign-up.component.html',
  styleUrl: './sign-up.component.scss'
})
export class SignUpComponent {
  signUpForm = new FormGroup({
    name: new FormControl('', [
      Validators.required,
      Validators.maxLength(50)
    ]),
    email: new FormControl('', [
      Validators.required,
      Validators.pattern('[a-zA-Z ]*')
    ]),
    password: new FormControl('', [
      Validators.required,
      Validators.minLength(8),
      Validators.pattern('^(?=.*[A-Z])(?=.*[!@#$%^&*]).{8,}$')
    ]),

    confirm: new FormControl('', [
      Validators.required,
      Validators.minLength(8),
      Validators.pattern('^(?=.*[A-Z])(?=.*[!@#$%^&*]).{8,}$')
    ]),


});
submitSignUp() {
  console.log(this.signUpForm.value);
}
 }
