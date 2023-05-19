import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CryptosComponent } from './cryptos.component';

describe('CryptosComponent', () => {
  let component: CryptosComponent;
  let fixture: ComponentFixture<CryptosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CryptosComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CryptosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
