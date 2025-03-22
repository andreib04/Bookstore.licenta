import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminMagazinesComponent } from './admin-magazines.component';

describe('AdminMagazinesComponent', () => {
  let component: AdminMagazinesComponent;
  let fixture: ComponentFixture<AdminMagazinesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AdminMagazinesComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdminMagazinesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
