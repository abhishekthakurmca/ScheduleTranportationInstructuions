import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ScheduleInstructionComponent } from './schedule-instruction.component';

describe('ScheduleInstructionComponent', () => {
  let component: ScheduleInstructionComponent;
  let fixture: ComponentFixture<ScheduleInstructionComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ScheduleInstructionComponent]
    });
    fixture = TestBed.createComponent(ScheduleInstructionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
