import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthRestService } from 'src/app/services/auth-rest.service';
import { Users } from '../../models/Users';

@Component({
  selector: 'app-editprofile',
  templateUrl: './editprofile.component.html',
  styleUrls: ['./editprofile.component.css']
})
export class EditprofileComponent {
  user: Users = new Users();

  constructor(private authService: AuthRestService, private route: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
    this.loadUser();
  }

  loadUser(): void {
    const idTemp = this.route.snapshot.params['id'];

    this.authService.getUser(idTemp).subscribe((data: Users) => {
      this.user = data;
    });
  }

  saveChanges(): void {
    if (this.user && this.user.userID) {
      const userId = this.user.userID;
      this.authService.updateUser(userId, this.user).subscribe(
        () => {
          alert('Alterações salvas com sucesso!');
          this.router.navigate([this.router.url]);
          setTimeout(() => {
            window.location.reload();
          }, 1000);
        },
        (error) => {
          console.log(this.user)
          alert('Erro ao salvar as alterações. Tente novamente.');
        }
      );
    }
  }
}

