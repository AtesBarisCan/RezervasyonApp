import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MekanEkleComponent } from './mekan-ekle.component';

describe('MekanEkleComponent', () => {
  let component: MekanEkleComponent;
  let fixture: ComponentFixture<MekanEkleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MekanEkleComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MekanEkleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
