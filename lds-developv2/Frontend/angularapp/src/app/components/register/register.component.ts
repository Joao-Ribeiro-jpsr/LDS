import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthRestService } from 'src/app/services/auth-rest.service';
import { Users } from '../../models/Users';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  message: string = '';
  user: Users = new Users();

  constructor(private router: Router, private authService: AuthRestService) { }

  register(): void {

    this.user.isAdmin = false;
    this.user.isAccountActivated = false;
    this.user.userContactsID = 3;


    const birthDate = this.user.nascimento ? new Date(this.user.nascimento) : null;

    if (birthDate && birthDate.getFullYear() === 2023) {
      alert('Ano de nascimento nÃ£o pode ser 2023.');
      return;
    }


    this.authService.registerUser(this.user).subscribe(
      (data) => {
        alert('User registered successfully! Não se esqueça de verificar o email!');
        this.router.navigate(['/login']);
      },
      (error) => {
        console.error('Error registering user:', error);
        this.message = 'Failed to register user. Please try again.';
      }
    );
  }
}
