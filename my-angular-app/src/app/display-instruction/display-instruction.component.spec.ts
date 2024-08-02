import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DisplayInstructionComponent } from './display-instruction.component';

describe('DisplayInstructionComponent', () => {
  let component: DisplayInstructionComponent;
  let fixture: ComponentFixture<DisplayInstructionComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DisplayInstructionComponent]
    });
    fixture = TestBed.createComponent(DisplayInstructionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
