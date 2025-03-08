// login.component.ts
import { Component,  OnInit, inject } from '@angular/core';
import { FormBuilder,FormControl, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { UseAuthService } from '../../core/services/use-auth.service';
import { UserAuth } from '../../models/userAuth';



@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {

  private fb = inject(FormBuilder);
  private authService = inject(UseAuthService);
  private router = inject(Router);
  loginForm = new FormGroup({
    email: new FormControl('', [
      Validators.required,
      //Validators.pattern('[a-zA-Z ]*')
    ]),
    password: new FormControl('', [
      Validators.required,
      Validators.minLength(4),
      //Validators.pattern('^(?=.*[A-Z])(?=.*[!@#$%^&*]).{8,}$')
    ]),
    rememberMe : new FormControl(false)

  });

  submitData() :void {
    debugger;
    if (this.loginForm.invalid) {
      return;
    }
    const user: UserAuth = {
      email: this.loginForm.value.email ?? '',  // Converte undefined/null para ''
      password: this.loginForm.value.password ?? '',

    };

    this.authService.login(user).subscribe({
      next: (response) => {
        debugger;
        console.log('Token recebido:', response);
        //localStorage.setItem('token', response.token);
        this.router.navigate(['/dashboard']);
      },
      error: (err) => {
        debugger;
        console.error('Erro no login:', err);
      }
    });


  }
}
