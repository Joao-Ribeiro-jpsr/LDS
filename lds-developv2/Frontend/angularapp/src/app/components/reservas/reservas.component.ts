import { HttpClient } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import * as L from 'leaflet';
import { Recintos } from 'src/app/models/Recintos';
import { RecintosRestService } from 'src/app/services/recintos-rest.service';
import { DatePipe } from '@angular/common';
import { ReservaRestService } from '../../services/reserva-rest.service';
import { Reservas } from '../../models/Reservas';
import { RecintoFakeAPI } from '../../models/RecintoFakeAPI';
import { FakeApiRestService } from '../../services/fake-api-rest.service';


@Component({
  selector: 'app-reservas',
  templateUrl: './reservas.component.html',
  styleUrls: ['./reservas.component.css']
})
export class ReservasComponent implements OnInit {

  selectedDate: string = '';
  selectedHour: string | null = null;

  showButtons: boolean = false;
  showContinueButton: boolean = false;
  showContinuePaymentButton: boolean = false;

  currentHour: string = '';
  recinto: Recintos = new Recintos();
  recintoFakeAPI: RecintoFakeAPI = new RecintoFakeAPI();
  reserva: Reservas = new Reservas();

  message: string = '';
  userId: any = localStorage.getItem('id');
  reservaID: any;

  today: Date = new Date();
  minDate: string = this.today.toISOString().split('T')[0];

  constructor(private route: ActivatedRoute, private datePipe: DatePipe, private rest: RecintosRestService, private router: Router, private authService: ReservaRestService, private fakeApiRest: FakeApiRestService) { }

  ngOnInit(): void {
    var idTemp = this.route.snapshot.params['id'];
    this.rest.getRecinto(idTemp).subscribe((data: Recintos) => {
      this.recinto = data;
    })
    this.fakeApiRest.getRecinto(idTemp).subscribe((data: RecintoFakeAPI) => {
      this.recintoFakeAPI = data;
    })

  }

  // Função de utilidade para formatar a data como uma string no formato 'YYYY-MM-DD'
  private formatDate(date: Date): string {
    const year = date.getFullYear();
    const month = ('0' + (date.getMonth() + 1)).slice(-2);
    const day = ('0' + date.getDate()).slice(-2);
    return `${year}-${month}-${day}`;
  }

onDateInput() {
  const today = new Date();
  this.minDate = this.formatDate(today);

  const selectedDateObj = new Date(this.selectedDate);

  // Verifica se a data selecionada é anterior à data de hoje
  if (selectedDateObj < today) {
    // Se a data selecionada for anterior à data de hoje, ajusta para a data de hoje
    this.selectedDate = this.formatDate(today);
  }

  // Mostra os botões quando uma data é selecionada
  this.showButtons = true;
}

  handleButtonClick(buttonText: string) {
    // Handle button click logic here
    console.log("Data selecionada:", this.selectedDate);
    console.log("Hora escolhida:", buttonText);

    this.showContinueButton = true;
    this.selectedHour = buttonText;
  }

  goToPaymentPage() {
    this.router.navigate(['/pagamento'], { queryParams: { reservaId: this.reservaID } });
  }

  NoPayment() {
    alert('Estimado cliente, tem uma hora para efetuar o pagamento. A sua reserva encontra-se disponível no seu hitórico de reservas!')
    this.router.navigate(['/reserveHistory']);
  }

  getCurrentTime() {
    const currentDate = new Date();
    const formattedTime = this.datePipe.transform(currentDate, 'HH:mm:ss');
    console.log('Hora atual:', formattedTime);
  }

  createReserva() {
    const currentDate: Date = new Date();
    this.currentHour = this.datePipe.transform(currentDate, 'HH:mm') ?? '00:00';
    this.reserva.horaReserva = this.currentHour;
    this.reserva.dataInicial = this.selectedDate;
    this.reserva.horaJogo = this.selectedHour ?? '00:00';
    this.reserva.preco = this.recinto.preco;
    this.reserva.userID = this.userId;
    this.reserva.estado = 'Pendente';
    this.reserva.recintoID = this.recinto.recintoID


    this.authService.criarReserva(this.reserva).subscribe(
      (data) => {
        alert('Reserva criada!');
        this.showContinuePaymentButton = true;
        this.reservaID = JSON.stringify(data);
      },
      (error) => {
        console.error('A reserva não pôde ser criada:', error);

        if (error.status === 400 && error.error) {
          this.message = error.error;
          this.router.navigate([this.router.url]);
          setTimeout(() => {
            window.location.reload();
          }, 3000);
        } else {
          this.message = 'A reserva não pôde ser criada. Tente novamente.';
        }

      }
    );

  }

}
