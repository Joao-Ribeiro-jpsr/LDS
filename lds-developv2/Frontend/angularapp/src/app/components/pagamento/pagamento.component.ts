import { ChangeDetectorRef, Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ReservaRestService } from '../../services/reserva-rest.service';
import { Reservas } from '../../models/Reservas';
import { PagamentosRestService } from '../../services/pagamentos-rest.service';
import { Pagamento } from '../../models/Pagamentos';
import { Users } from '../../models/Users';
import { AuthRestService } from '../../services/auth-rest.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ConfirmarUtilizacaoPontosModalComponent } from '../../confirmar-utilizacao-pontos-modal/confirmar-utilizacao-pontos-modal.component';

declare const paypal: any;

@Component({
  selector: 'app-pagamento',
  templateUrl: './pagamento.component.html',
  styleUrls: ['./pagamento.component.css']
})
export class PagamentoComponent implements OnInit {

  @Input() reserva?: Reservas | undefined;
  pontosUser: number = 0;
  pontosUtilizados: number = 0;
  totalParaExibir: string = '';
  message: string = '';
  pagamento: Pagamento = new Pagamento();
  userId: any = localStorage.getItem('id');

  constructor(
    private route: ActivatedRoute,
    private rest: PagamentosRestService,
    private router: Router,
    private reservarest: ReservaRestService,
    private authService: AuthRestService,
    private modalService: NgbModal
  ) { }

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      const idTemp = params['reservaId'];
      this.reservarest.getReserva(idTemp).subscribe((data: Reservas | undefined) => {
        if (data && data.preco !== undefined) {
          this.reserva = data;
          this.totalParaExibir = this.reserva && this.reserva.preco !== undefined
            ? `€${this.reserva.preco.toFixed(2)}`
            : 'Preço Indefinido';
        } else {
          console.error('Reserva ou preço não definidos para inicializar totalParaExibir.');
        }
        this.initPayPalButton();
      });
    });

    if (this.userId) {
      this.authService.getUser(this.userId).subscribe((data: Users) => {
        this.pontosUser = data.pontos || 0;
      });
    }
  }

  calcularTotalComDesconto(pontosUtilizados: number): number {
    if (this.reserva && this.reserva.preco !== undefined) {
      const descontoPercentagem = 0.5;
      const desconto = pontosUtilizados * descontoPercentagem;
      return Math.max(0, this.reserva.preco - ((this.reserva.preco * desconto) / 100));
    } else {
      console.error('Reserva ou preço não definidos para calcular o total com desconto.');
      return 0;
    }
  }

  mostrarTotal(): string {
    return this.totalParaExibir;
  }

  utilizarPontos() {
    if (this.pontosUser > 0) {
      const pontosUtilizadosStr = prompt(`Quantidade de pontos a serem utilizados (máximo ${this.pontosUser}):`);
      if (pontosUtilizadosStr !== null) {
        const pontosUtilizadosNum = parseInt(pontosUtilizadosStr, 10);

        if (!isNaN(pontosUtilizadosNum) && pontosUtilizadosNum > 0 && pontosUtilizadosNum <= this.pontosUser) {
          this.totalParaExibir = `€${this.calcularTotalComDesconto(pontosUtilizadosNum).toFixed(2)} (com desconto)`;
          this.pontosUser -= pontosUtilizadosNum;
          this.pontosUtilizados = pontosUtilizadosNum;
        } else {
          alert('Por favor, insira uma quantidade válida de pontos.');
        }
      }
    }
  }

  createPagamento() {
    if (this.reserva) {
      this.pagamento.userID = this.userId;
      this.pagamento.total = this.calcularTotalComDesconto(this.pontosUtilizados);
      this.pagamento.metodoID = 2;

      this.rest.createPagamento(this.reserva?.reservaID || 0, this.pagamento, this.pontosUtilizados).subscribe(
        (data) => {
          alert('Pagamento Criado!');
          this.router.navigate(['']);
        },
        (error) => {
          console.error('O pagamento não pôde ser criado:', error);
          console.log(error.error);
          this.message = 'O pagamento não pôde ser efetuado. Tente novamente.';
        }
      );
    }
  }

  initPayPalButton() {
    if (this.reserva && this.reserva.preco !== undefined) {
      paypal.Buttons({
        createOrder: (data: any, actions: any) => {
          return actions.order.create({
            purchase_units: [
              {
                amount: {
                  value: this.calcularTotalComDesconto(this.pontosUtilizados).toString(),
                  currency: 'EUR'
                },
              },
            ],
          });
        },
        onApprove: (data: any, actions: any) => {
          return actions.order.capture().then((details: any) => {
            console.log('Detalhes do pagamento PayPal:', details);
            this.createPagamento();
          });
        },
        onError: (error: any) => {
          console.error('Erro no pagamento PayPal:', error);
        }
      }).render('#paypal-button-container');
    }
  }
}
