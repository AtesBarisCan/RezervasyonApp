import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MekanDetayComponent } from './mekan-detay.component';

describe('MekanDetayComponent', () => {
  let component: MekanDetayComponent;
  let fixture: ComponentFixture<MekanDetayComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MekanDetayComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MekanDetayComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
