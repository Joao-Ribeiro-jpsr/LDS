import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfirmarUtilizacaoPontosModalComponent } from './confirmar-utilizacao-pontos-modal.component';

describe('ConfirmarUtilizacaoPontosModalComponent', () => {
  let component: ConfirmarUtilizacaoPontosModalComponent;
  let fixture: ComponentFixture<ConfirmarUtilizacaoPontosModalComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ConfirmarUtilizacaoPontosModalComponent]
    });
    fixture = TestBed.createComponent(ConfirmarUtilizacaoPontosModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
