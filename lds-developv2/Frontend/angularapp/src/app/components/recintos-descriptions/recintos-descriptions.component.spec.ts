import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RecintosDescriptionsComponent } from './recintos-descriptions.component';

describe('RecintosDescriptionsComponent', () => {
  let component: RecintosDescriptionsComponent;
  let fixture: ComponentFixture<RecintosDescriptionsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RecintosDescriptionsComponent]
    });
    fixture = TestBed.createComponent(RecintosDescriptionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
