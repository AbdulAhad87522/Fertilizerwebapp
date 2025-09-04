import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SellerForm } from './seller-form';

describe('SellerForm', () => {
  let component: SellerForm;
  let fixture: ComponentFixture<SellerForm>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SellerForm]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SellerForm);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
