import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { Recintos } from 'src/app/models/Recintos';
import { Rating } from 'src/app/models/Rating';
import { RateRestService } from 'src/app/services/rate-rest.service';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';


@Component({
  selector: 'app-avaliacoes',
  templateUrl: './avaliacoes.component.html',
  styleUrls: ['./avaliacoes.component.css']
})
export class AvaliacoesComponent {

  ratings: any;
  rate: Rating = new Rating();

  @Input() recintoID?: number;
  userId: any = localStorage.getItem('id');

  selectedRating: number = 0;

  username?: string;
  @Input() description?: string;

  showCard = true;
  showForm: boolean = false;
  message: string = '';


  constructor(private sanitizer: DomSanitizer, private rest: RateRestService, private route: ActivatedRoute, private router: Router, private http: HttpClient) {
  }

  ngOnInit(): void {
    this.rest.getRatings(this.recintoID).subscribe(
      (data) => {
        this.ratings = data;

      },
      (error) => {
        this.message = 'Ainda não existem ratings para este recinto';
        console.log(error);
      }
    )
    
  }

  toggleForm() {
    this.showForm = !this.showForm;
  }

  setRating(rating: number) {
    this.selectedRating = rating;
  }

  getSelectedStars(selectedRating: number): SafeHtml {
    var stars = "";
    for (var i = 1; i <= 5; i++) {
      if (i <= selectedRating) {
        stars += "\u2605";
      } else {
        stars += "\u2606";
      }
    }
    return this.sanitizer.bypassSecurityTrustHtml(stars);
  }
  

  getStars(rating: number): SafeHtml {
    var stars = "";
    for (var i = 1; i <= 5; i++) {
      if (i <= rating) {
        stars += "\u2605"; 
      } else {
        stars += "\u2606"; 
      }
    }
    return this.sanitizer.bypassSecurityTrustHtml(stars);
  }

  Rate(): void {
    console.log(this.recintoID);

    if (this.recintoID !== undefined) { // verifique se não é indefinido ou nulo
      this.rate.recintoID = this.recintoID;
      this.rate.rating = this.selectedRating;
      this.rate.userID = this.userId;
      this.rate.username = this.username;
      this.rate.description = this.description;

      this.rest.createRate(this.rate).subscribe(
        (data) => {
          alert('Rating registado!');
          this.router.navigate([this.router.url]);
          setTimeout(() => {
            window.location.reload();
          }, 1000);
        },
        (error) => {
          console.error('O rating não pôde ser criado:', error);
          console.log(error.error); // Verificar se há detalhes sobre erros de validação
          this.message = 'O rating não pôde ser criado. Tente novamente.';
          console.log(this.ratings);
        }
      );
    } else {
      console.error('recintoID não definido.');
    }
  }


}
