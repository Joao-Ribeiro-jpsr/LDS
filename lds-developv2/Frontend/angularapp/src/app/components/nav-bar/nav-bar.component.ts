import {
  Component, HostListener
  , Input,
  OnInit
} from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { Recintos } from 'src/app/models/Recintos';
import { RecintosRestService } from 'src/app/services/recintos-rest.service';
import { Users } from '../../models/Users';
import { AuthRestService } from '../../services/auth-rest.service';
import { ReservaRestService } from '../../services/reserva-rest.service'; 
import { Reservas } from '../../models/Reservas';


@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {

  @Input() search?: string = '';
  userId: any = localStorage.getItem('id');
  @Input() user?: Users;
  reservasCount?: number;


  ngOnInit(): void {
    this.loadUser();
    this.getPendentReservesCount();
  }

  loadUser(): void {

    this.authService.getUser(this.userId).subscribe((data: Users) => {
      this.user = data;
    });

  }
  

  constructor(private router: Router, private authService: AuthRestService, private route: ActivatedRoute, private reservaService: ReservaRestService) {

  }

  logout() {
    alert('Successfully logged out!');
    localStorage.removeItem('id');
    this.router.navigate(['/login']);
  }

  ViewPerfil(id: any) {
    this.router.navigate(['/profile/' + id]);
  }

  getPendentReservesCount() {
    this.reservaService.getPendentReserves(this.userId).subscribe(
      (reservasCount: any) => {
        this.reservasCount = reservasCount;
      },
      error => {
        console.error('Erro ao obter a contagem de reservas pendentes:', error);
      }
    );
  }

  searchAndNavigate(): void {
    const searchTerm = this.search?.trim();
    if (searchTerm !== '' && searchTerm !== undefined) {
      this.router.navigate(['/recintos'], { queryParams: { concelho: searchTerm } });
    }
  }

  onInput(event: any): void {
    this.search = event.target.value;
  }

  @HostListener('document:keydown', ['$event'])
  handleKeyboardEvent(event: KeyboardEvent): void {
    if (event.key === 'Enter') {
      this.searchAndNavigate();
    }
  }
}
