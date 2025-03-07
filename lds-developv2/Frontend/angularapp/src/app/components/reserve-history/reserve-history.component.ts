import { HttpClient } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import * as L from 'leaflet';
import { Recintos } from 'src/app/models/Recintos';
import { RecintosRestService } from 'src/app/services/recintos-rest.service';
import { DatePipe } from '@angular/common';
import { ReservaRestService } from '../../services/reserva-rest.service';
import { Reservas } from '../../models/Reservas';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
@Component({
  selector: 'app-reserve-history',
  templateUrl: './reserve-history.component.html',
  styleUrls: ['./reserve-history.component.css']
})
export class ReserveHistoryComponent implements OnInit {
  recintos: Recintos[] = [];
  reservas: Reservas[] = [];
  userId: any = localStorage.getItem('id');
  message: string = '';

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  // Propriedades para controle de paginação
  pageSize = 5;
  pageIndex = 0;
  pageLength = 0;
  constructor(private rest: ReservaRestService, private recintoRest: RecintosRestService, private router: Router) { }

  ngOnInit(): void {
    this.rest.getReserveHistory(this.userId).subscribe((data: any) => {
      this.reservas = data;
      this.pageLength = this.reservas.length;
      this.sortReservasByRecinto();
      this.applyPaginator();
    });
  }

  applyPaginator(): void {
    this.paginator.pageSize = this.pageSize;
    this.paginator.pageIndex = this.pageIndex;
    this.paginator.length = this.pageLength;
  }

  onPageChange(event: PageEvent): void {
    this.pageIndex = event.pageIndex;
  }

  private sortReservasByRecinto(): void {
    this.reservas.forEach((reserva, index) => {
      this.recintoRest.getRecinto(reserva.recintoID).subscribe((recinto: Recintos) => {
        this.recintos[index] = recinto;
        
        console.log(`Recinto para reserva ${index + 1}:`, recinto);

        if (this.recintos.length === this.reservas.length) {
          console.log('Todos os recintos foram atribuídos.');
        }
        this.reservas = this.reservas.sort((a, b) => {
          if (a.reservaID && b.reservaID) {
            return b.reservaID - a.reservaID;
          }
          return -1;
        }); 
      });
    });
  }

  cancelarReserva(reservaId?: number): void {
    const reserva = this.reservas.find((reserva) => reserva.reservaID === reservaId);
    if (reserva?.estado === "Cancelada") {
      this.message = "Reserva já se encontra cancelada!";
    } else {
      this.rest.cancelarReserva(reservaId).subscribe((data: any) => {
        this.message = data;
        this.router.navigate([this.router.url]);
        setTimeout(() => {
          window.location.reload();
        }, 1000);
      })
    }
  }

  goToRecinto(id: any) {
    this.router.navigate(['/recintos/' + id]);
  }
  /**
   * Este método vai para pagamento se a reserva se encontrar pendente e se não estiver expirada.
   * @param id
   */
  goToPayment(id: any ) {
    this.rest.expirarReserva(this.reservas).subscribe((reservas: any) => {
      const reserva = reservas.find((reserva: any) => reserva.reservaID === id);
      if (reserva && reserva.estado === "Pendente") {
        this.router.navigate(['/pagamento'], { queryParams: { reservaId: id } });
      } else {
        this.message = "A reserva econtra-se expirada!";
      }
    });
  }
}
