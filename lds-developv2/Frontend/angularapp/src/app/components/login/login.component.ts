import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthRestService } from 'src/app/services/auth-rest.service';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit{

  email!: string;
  password!: string;
  public message!: string;
  userId: any;
  params!: string;


  constructor(private authService: AuthRestService, private router: Router, private route: ActivatedRoute) { }
  ngOnInit(): void {
    this.route.queryParams.subscribe((params) => {
      this.params = params['userId'];
    })
  }

  login(): void { 
    let query;
    if (this.params) {
      query = '?userId=' + this.params; 
    }
    this.authService.loginUser(this.email, this.password, query).subscribe(
      (data) => {
        alert("Login Sucessful")
        if (data) {
          localStorage.setItem('id', JSON.stringify(data));
          this.router.navigate(['/']);
        } else {
          
          this.message = 'Invalid email or password. Please try again.';
        }
      },
      (error) => {
        console.error('Login failed:', error);
        this.message = error.error;
      }
    );
  }
}



