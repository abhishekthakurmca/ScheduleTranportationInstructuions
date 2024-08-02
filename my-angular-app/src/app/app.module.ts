import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { DxButtonModule, DxDataGridModule, DxDropDownBoxModule, DxFormModule, DxLookupModule, DxSelectBoxModule, DxToolbarModule, DxTemplateModule  } from 'devextreme-angular';
import { InstructionComponent } from './new-instruction/instruction.component';
import { DisplayInstructionComponent } from './display-instruction/display-instruction.component';
import { ScheduleInstructionComponent } from './schedule-instruction/schedule-instruction.component';
import { HttpClientModule } from '@angular/common/http';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { DxoMasterDetailComponent } from 'devextreme-angular/ui/nested';

const TOASTER_SETTINGS = {
  timeOut: 3000,
  positionClass: 'toast-bottom-right',
  preventDuplicates: false,
  easing: 'ease-in-out',
};

@NgModule({
  declarations: [
    AppComponent,
    InstructionComponent,
    DisplayInstructionComponent,
    ScheduleInstructionComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    DxDataGridModule,
    DxButtonModule,
    DxFormModule,
    DxDropDownBoxModule,
    DxSelectBoxModule, 
    DxToolbarModule,
    DxLookupModule,
    DxTemplateModule,
    HttpClientModule,
    ToastrModule.forRoot(TOASTER_SETTINGS),
    BrowserAnimationsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
