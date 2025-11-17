import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LawyerStatsComponent } from './lawyer-stats.component';

describe('LawyerStatsComponent', () => {
  let component: LawyerStatsComponent;
  let fixture: ComponentFixture<LawyerStatsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LawyerStatsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LawyerStatsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
