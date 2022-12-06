import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdicionarrotaComponent } from './adicionarrota.component';

describe('AdicionarrotaComponent', () => {
  let component: AdicionarrotaComponent;
  let fixture: ComponentFixture<AdicionarrotaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AdicionarrotaComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdicionarrotaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
