import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RecintosComponent } from './recintos.component';

describe('RecintosComponent', () => {
  let component: RecintosComponent;
  let fixture: ComponentFixture<RecintosComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RecintosComponent]
    });
    fixture = TestBed.createComponent(RecintosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
