import { DatePipe } from "@angular/common";
import { Data } from "ngx-bootstrap/positioning/models";

export class Reservas {
  constructor(
    public reservaID?: number, // id da reserva
    public recintoID?: number, // id do recinto da reserva
    public userID?: number, // id do user da reserva
    public userContactsID?: number,
    public pagamentoID?: number, // id do pagamento da reserva
    public dataInicial?: string, // dataInicial da reserva
    public horaReserva?: string, // Hora no momento da reserva
    public horaJogo?: string, // Hora marcada da reserva
    public horaCancelamento?: string, // Hora marcada do cancelamento da reserva
    public preco?: number, // Preço da reserva
    public estado?: string // Estado da reserva Pendente/Cancelada/Confirmada
  ) { }
}
