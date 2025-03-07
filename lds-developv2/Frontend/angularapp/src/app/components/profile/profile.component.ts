
import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AuthRestService } from 'src/app/services/auth-rest.service';
import { Users } from 'src/app/models/Users';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  @Input() user?: Users;

  edit?: boolean;

  constructor(private authService: AuthRestService, private route: ActivatedRoute) { }


  ngOnInit(): void {
    this.loadUser();
  }

  loadUser(): void {
    const idTemp = this.route.snapshot.params['id'];

    this.authService.getUser(idTemp).subscribe((data: Users) => {
      this.user = data;
    });

  }

  editUser(value: boolean): void {
    this.edit = value;
  }
}

