import { Component, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-confirmar-utilizacao-pontos-modal',
  templateUrl: './confirmar-utilizacao-pontos-modal.component.html',
  styleUrls: ['./confirmar-utilizacao-pontos-modal.component.css']
})
export class ConfirmarUtilizacaoPontosModalComponent {
  @Input() pontosAtuais: number = 0;

  constructor(public activeModal: NgbActiveModal) { }

  confirmar() {
    // Confirma o uso de pontos
    this.activeModal.close('confirmado');
  }

  cancelar() {
    // Cancela o uso de pontos
    this.activeModal.dismiss('cancelado');
  }
}
