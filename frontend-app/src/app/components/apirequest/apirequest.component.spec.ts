import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ApirequestComponent } from './apirequest.component';

describe('ApirequestComponent', () => {
  let component: ApirequestComponent;
  let fixture: ComponentFixture<ApirequestComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ApirequestComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ApirequestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
