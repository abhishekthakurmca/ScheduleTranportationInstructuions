import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { InstructionComponent } from './new-instruction/instruction.component';
import { DisplayInstructionComponent } from './display-instruction/display-instruction.component';
import { ScheduleInstructionComponent } from './schedule-instruction/schedule-instruction.component';

const routes: Routes = [
  { path: "newinstruction", component: InstructionComponent},
  { path: "home", component: DisplayInstructionComponent},
  { path: "scheduleinstruction", component: ScheduleInstructionComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
