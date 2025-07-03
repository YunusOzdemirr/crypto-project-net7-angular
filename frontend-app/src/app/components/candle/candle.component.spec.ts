import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CandleComponent } from './candle.component';

describe('CandleComponent', () => {
  let component: CandleComponent;
  let fixture: ComponentFixture<CandleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CandleComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CandleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
