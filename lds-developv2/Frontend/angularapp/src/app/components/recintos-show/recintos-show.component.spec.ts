import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RecintosShowComponent } from './recintos-show.component';

describe('RecintosShowComponent', () => {
  let component: RecintosShowComponent;
  let fixture: ComponentFixture<RecintosShowComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RecintosShowComponent]
    });
    fixture = TestBed.createComponent(RecintosShowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
