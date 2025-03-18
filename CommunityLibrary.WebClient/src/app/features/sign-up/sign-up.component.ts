import { Component,inject } from '@angular/core';
import { FormBuilder, FormGroup, FormControl ,ReactiveFormsModule, Validators,  } from '@angular/forms';
import { IUser } from '../../models/Iuser';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { UserService  } from '../../core/services/user.service';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-sign-up',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule,
    ToastModule],
  templateUrl: './sign-up.component.html',
  styleUrl:  './sign-up.component.scss',
  providers: [MessageService]
})

export class SignUpComponent {
  constructor(private messageService: MessageService) {}
private fb = inject(FormBuilder);
  private router = inject(Router);
  private userService = inject(UserService);
  signUpForm =new FormGroup({
    name: new FormControl('',
      [Validators.required,
        Validators.minLength(3)]),
    email: new FormControl('',
      [Validators.required,
       Validators.email,
       Validators.pattern('[a-zA-Z ]*')]),
    password: new FormControl('',
      [Validators.required,
      Validators.minLength(8),
      Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@#$%^&*!?]).{8,}$/)]),
      confirmedPassword:  new  FormControl('', [Validators.required,
        Validators.minLength(8),
        Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@#$%^&*!?]).{8,}$/)])
  });



  submitSignUp(): any{

    if (this.signUpForm.valid) {
      console.log('Form Data:', this.signUpForm.value);
      return;
    }
    const user: IUser = {
      name: this.signUpForm.value.name?? '',
      email: this.signUpForm.value.email?? '',
      password: this.signUpForm.value.password?? '',
      confirmedPassword : this.signUpForm.value.confirmedPassword?? '',
      createDate: new Date().toISOString()
      }

      debugger;
      this.userService.create(user).subscribe({
        next: (response) => {
          debugger;
          if(response.success)
            this.messageService.add({ severity: 'success', summary: 'Login Realizado', detail: response.message });
          else{
            this.messageService.add({ severity: 'error', summary: 'Login Realizado', detail: 'Bem-vindo ao sistema!' });
          }
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
